@echo off

if "%log%"=="" (git commit -m "�Զ��ύ") else (git commit -m %log%)
git push

echo "================push�ɹ�========================"

pause