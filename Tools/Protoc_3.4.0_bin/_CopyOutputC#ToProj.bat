@echo off

echo "开始拷贝"
for %%i in (Output\*) do (
     echo begin copy...%%i
     copy /y Output\%%~nxi ..\..\Assets\UnityWebsocket\Scripts\ProtoMsg\%%~nxi
     echo copy complate ... %%i
)

pause