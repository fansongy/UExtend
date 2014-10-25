#!/usr/bin/env python
#-*- coding:utf-8 -*-

import glob
import json
import os
import sys,getopt
import codecs

"""
Sound auto make to config
"""

fileDir = "."
fileExport = "../Configs/soundlist.txt"
extends = ["*.AIFF","*.WAV","*.MP3","*.OGG",
		   "*.aiff","*.wav","*.mp3","*.ogg"]

def main(argv):

	files = []
	#find all file in sound dircotry
	for ext in extends:
		fileList = glob.glob(ext)
		if len(fileList) >0:
			files += fileList
	print files

	#make Dict
	fileDict = {}
	for each in files:
		key = each.split(".")[0].decode("utf-8")
		fileDict[key] = each.decode("utf-8")
	print fileDict

	#write to Json
	with codecs.open(fileExport,'w','utf-8') as w:
				output = json.dump(fileDict,w,ensure_ascii = False,indent = 4,sort_keys=True)

if __name__ == '__main__':
	main(sys.argv)