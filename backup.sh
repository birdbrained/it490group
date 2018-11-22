#!/bin/bash
cd ..
# tar -czf backup.tgz it490group/
OUTTAR="backup"
INDIR="it490group/"

if [ "$1" = "php" ]; then
	OUTTAR="${OUTTAR}php"
	INDIR="${INDIR}*.php"
	# tar -czf backupphp.tgz it490group/*.php
fi
OUTTAR="${OUTTAR}.tgz"
# echo $OUTTAR
# echo $INDIR

tar -czf ${OUTTAR} $INDIR
