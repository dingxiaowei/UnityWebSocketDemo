@echo off

echo "��ʼ����"
for %%i in (client\*) do (
    echo begin copy... %%i
    copy /y client\%%~nxi ..\..\Assets\UnityWebSocket\Scripts\ProtoMsg\%%~nxi
    echo copy complate ... %%i
)
echo "�������"

pause