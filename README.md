RTMPExploreX
============
RTMPExploreX (by Andr3jx) is an improved version of RTMPExplorer ( http://bit.ly/J24rky ). 
RTMPExploreX is a freeware GUI for rtmpsrv/rtmpsuck. RTMPexploreX routes all RTMP/RTMPE traffic to rtmpsrv/rtmpsuck. This allows to download RTMP and RTMPE streams using RTMPdump automatically.
It is open source, so everybody can contribute to the project. The main part of the code was decompiled from RTMPExplorer. It makes use of EasyHook-library available here: http://easyhook.codeplex.com/ 
RTMPExploreX is an extended userfriendlier version of RTMPExplorer.

How to use:
- !! Copy rtmpdump, rtmpsrv(-vlc), rtmpsuck to the directory of RTMPExploreX. You can download them e.g. here: http://www.videohelp.com/download/rtmpdump-v2.5.zip
- Start RTMPExploreX
- Navigate to the page with RTMP(E)-Stream
- Choose RTMP-Tool and click 'Monitor' BEFORE the stream starts (sometimes some adverts play before your stream starts, in this case click 'Monitor' when the advert already started)
- RTMPExploreX will start a batch-file and rtmpsrv will run in cmd.exe
- If you want to change the running RTMP-Tool: Close cmd.exe, Click 'Disconnect', Choose RTMP-Tool and click 'Monitor' again
- If you don't want to use the batch-files choose always cmd.exe to ignore them; You can start rtmpsrv/rtmpsuck from any directory
- If the stream connects to port 1935(or your specified port), RTMPExploreX will pass all needed parameters to rtmpsrv and if there are no special protection methods (like authentication tokens) it should work
- Once you see in cmd.exe that the stream is recording you can click 'Disconnect' to prevent that RTMPExploreX sends other connections which can lead to problems
- While rtmpdump is recording the stream you can always watch it by opening the video-file with VLC-Player
- Keep in mind that RTMPExploreX uses Internet Explorer as browser, so it's not very stable and some pages could be displayed wrong
- The batch files create the file Commands.txt which should contain all passed parameters. You can use them if you want to establish a connection manual and change parameters (Commands.txt is also displayed if you click 'Log')
- If you use rtmpsuck you can only see the output in rtmpsuck-log.txt (click 'Log' to see the file) However you can use and copy the output of rtmpsuck easy
- If a stream is not seen by RTMPExploreX, the reason can be that it doesn't use port 1935. Use a network sniffer to find the port you need and change it under 'RTMP-Port'
- If a stream is available in different resolution RTMPExploreX will probably not be able to pass the parameters for stream with the best resolution - In this case use e.g. URL Snooper to find the rtmp-url for high resolution and use Commands.txt to replace the url; Delete the -y parameter and replace the -r parameter. Keep in mind that RTMPExploreX passes also unnecessary parameters which can be left out

These are parameters generated by RTMPExploreX. It is a stream with 384x216 resolution and contains unnecessary parameters:

> -r "rtmpe://cp82245.edgefcs.net:1935/ondemand" -a "ondemand?ovpfv=2.1.4" -f "WIN 11,2,202,235" -W "http://media.mtvnservices.com/player/prime/mediaplayerprime.1.8.1.swf" -p "about:blank" -C Z: -y "mp4:mtvnorigin/gsp.originmusicstor/sites/mtvi/music_videos/l/linkin_park/burn_it_down_hd_full_276490_384x216_400_m30.mp4" -o burn_it_down_384x216.flv

These are parameters after I edited them. I sniffed the URL for 1280x720 resolution and deleted the unnecessary parts:
> -r "rtmpe://cp82245.edgefcs.net/ondemand/mtvnorigin/gsp.originmusicstor/sites/mtvi/music_videos/l/linkin_park/burn_it_down_hd_full_276490_1280x720_3500_h32.mp4"  -W "http://media.mtvnservices.com/player/prime/mediaplayerprime.1.8.1.swf" -o burn_it_down_1280x720.flv
