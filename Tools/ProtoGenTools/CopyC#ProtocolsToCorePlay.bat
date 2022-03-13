@echo off

echo "开始拷贝"
for %%i in (client\*) do (
    echo begin copy... %%i
    copy /y client\%%~nxi ..\..\Assets\UnityWebSocket\Scripts\ProtoMsg\%%~nxi
    echo copy complate ... %%i
)
echo "拷贝完成"

pause