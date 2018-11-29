#!/bin/bash
cd ..
# tar -czf backup.tgz it490group/
OUTTAR="backup"
INDIR="it490group/"

if [ "$1" = "server" ]; then
	OUTTAR="${OUTTAR}_server"
	INDIR="${INDIR}serverFiles.ini"
fi
OUTTAR="${OUTTAR}_v$2.tgz"
 echo $OUTTAR
# echo $INDIR

tar -zcvf ${OUTTAR} --files-from $INDIR
mv ${OUTTAR} it490group/backups
