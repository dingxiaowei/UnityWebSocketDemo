@echo off

echo "��ʼ����"
for %%i in (client\*) do (
    echo begin copy... %%i
    copy /y client\%%~nxi ..\..\hola_library\CorePlay\WSSocket\NetProtocols\%%~nxi
    echo copy complate ... %%i
)
echo "�������"

pause