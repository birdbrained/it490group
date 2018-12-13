#!/bin/bash
cd ..
# tar -czf backup.tgz it490group/
OUTTAR="backup"
INDIR="it490group/"

if [ "$1" = "server" ]; then
	OUTTAR="${OUTTAR}_server"
	INDIR="${INDIR}serverFiles.ini"
elif [ "$1" = "client" ]; then
	OUTTAR="${OUTTAR}_client"
	INDIR="${INDIR}clientFiles.ini"
elif [ "$1" = "frontend" ]; then
	OUTTAR="${OUTTAR}_frontend"
	INDIR="${INDIR}frontFiles.ini"
fi
OUTTAR="${OUTTAR}_v$2.tgz"
 echo $OUTTAR
# echo $INDIR

tar -zcvf ${OUTTAR} --files-from $INDIR
mv ${OUTTAR} it490group/backups
