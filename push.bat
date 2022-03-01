@echo off

if "%log%"=="" (git commit -m "自动提交") else (git commit -m %log%)
git push

echo "================push成功========================"

pause