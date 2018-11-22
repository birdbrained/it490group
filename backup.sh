#!/bin/bash
cd ..
# tar -czf backup.tgz it490group/
OUTTAR="backup"
INDIR="it490group/"

if [ "$1" = "server" ]; then
	OUTTAR="${OUTTAR}_server"
	INDIR="${INDIR}serverFiles.txt"
	# tar -czf backupphp.tgz it490group/*.php
fi
OUTTAR="${OUTTAR}.tgz"
# echo $OUTTAR
# echo $INDIR

tar -zcvf ${OUTTAR} --files-from $INDIR
