@echo off
for %%i in (*.proto) do (
   echo gen %%~nxi...
   tool\protoc.exe --csharp_out=Output  %%~nxi)

echo finish... 
pause