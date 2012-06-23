// portforward.cpp : trivial port forwarding for windows
//

#include <winsock2.h>
#include <stdio.h>

DWORD WINAPI reader(LPVOID lpParameter)
{
    SOCKET *socks = (SOCKET*)lpParameter;

    char buf[65536];
    int n;
    while ((n = recv(socks[0], buf, sizeof(buf), 0)) > 0) {
        send(socks[1], buf, n, 0);
    }

    closesocket(socks[0]);
    closesocket(socks[1]);

    return 0;
}

DWORD WINAPI writer(LPVOID lpParameter)
{
    SOCKET *socks = (SOCKET*)lpParameter;

    char buf[65536];
    int n;
    while ((n = recv(socks[1], buf, sizeof(buf), 0)) > 0) {
        send(socks[0], buf, n, 0);
    }

    closesocket(socks[0]);
    closesocket(socks[1]);

    return 0;
}

int main(int argc, char* argv[])
{
    if (argc < 4) {
        printf("usage: portforward [port to listen on] [ip of host to connect to] [port to connect to].\n");
        return 1;
    }

    WORD wVersionRequested;
    WSADATA wsaData;
 
    wVersionRequested = MAKEWORD( 2, 2 );
    WSAStartup( wVersionRequested, &wsaData );

    SOCKET s = socket(AF_INET, SOCK_STREAM, 0);

    sockaddr_in sin;
    sin.sin_addr.S_un.S_addr = INADDR_ANY;
    sin.sin_family = AF_INET;
    sin.sin_port = htons(atoi(argv[1]));
    if (bind(s, (sockaddr*)&sin, sizeof(sin)) != 0) {
        printf("cannot bind to port %i\n", atoi(argv[1]));
        return 1;
    }

    listen(s, 5);
    int ss = sizeof(sin);
    SOCKET n;
    while ((n = accept(s, (sockaddr*)&sin, &ss)) != -1) {
        printf("received connection from %i.%i.%i.%i\n", sin.sin_addr.S_un.S_un_b.s_b1, sin.sin_addr.S_un.S_un_b.s_b2, sin.sin_addr.S_un.S_un_b.s_b3, sin.sin_addr.S_un.S_un_b.s_b4);
        SOCKET d = socket(AF_INET, SOCK_STREAM, 0);
        sin.sin_family = AF_INET;
        sin.sin_addr.S_un.S_addr = inet_addr(argv[2]);
        sin.sin_port = htons(atoi(argv[3]));
        if (connect(d, (sockaddr*)&sin, sizeof(sin)) != 0) {
            printf("received a connection but can't connect to %s:%i\n", argv[2], atoi(argv[3]));
            closesocket(n);
        } else {
            printf("connection to %s:%i established\n", argv[2], atoi(argv[3]));
            SOCKET *socks = new SOCKET[2];
            socks[0] = n;
            socks[1] = d;
            DWORD id;
            CreateThread(NULL, 0, reader, socks, 0, &id);
            CreateThread(NULL, 0, writer, socks, 0, &id);
        }
    }
    closesocket(s);

    return 0;
}

