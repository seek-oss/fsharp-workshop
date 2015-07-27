@echo off
cls

..\.paket\paket.bootstrapper.exe 1.18.5
if errorlevel 1 (
  exit /b %errorlevel%
)

..\.paket\paket.exe restore
if errorlevel 1 (
  exit /b %errorlevel%
)

packages\FAKE\tools\FAKE.exe build.fsx %*