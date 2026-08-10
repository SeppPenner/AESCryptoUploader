cd ..\src

@ECHO off
cls

FOR /d /r . %%d in (bin,obj) DO (
	IF EXIST "%%d" (		 	 
		ECHO %%d | FIND /I "\node_modules\" > Nul && ( 
			ECHO.Skipping: %%d
		) || (
			ECHO.Deleting: %%d
			rd /s/q "%%d"
		)
	)
)

@ECHO on
@ECHO.Publishing self contained for win-x64...
@cd AESCryptoUploader
@dotnet publish -c Release -r win-x64 --self-contained true -o bin/publish
@ECHO.Deleting *.pdb files...
@cd bin/publish
@del *.pdb
@ECHO.Build successful. Press any key to exit.
pause