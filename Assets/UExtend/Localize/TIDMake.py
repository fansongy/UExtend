#!/usr/bin/env python
#-*- coding:utf-8 -*-

import json
import sys,getopt
import codecs

"""
TIDMake script help to make localize program.

"""

path = "test.json"
langs = {"CN","EN","RU"}
defaultLang = "CN"


def Usage():
	print "Usage: TIDMake [-options] [args]"
	print "\t-a [tidName] : add a new TID"
	print "\t-d [describe]: add a describe for the TID,default is chinese.\n\t\t\tWarning: It should after '-a' or '-c' "
	print "\t-e : export language for translate"

def addTID(tid):
	try:
		with open(path,'r') as f:
			jsonData = json.load(f)
			if(jsonData.has_key(tid)):
				print "The TID: "+tid+" already have"
				f.close()
				sys.exit(2)
			#add new data
			newData = {}
			for types in langs:
				newData[types] = "???"
			jsonData[tid] = newData
			# jsonData = sorted(jsonData.items(),key=lambda d:d[0])
			with codecs.open(path,'w','utf-8') as w:
				output = json.dump(jsonData,w,ensure_ascii = False,indent = 4)
	except IOError as err:
		print "Fail to open File at "+path+" Error is: "+str(err)

def findTID(tid):
	return True

def main(argv):
	# reload(sys)
	# sys.setdefaultencoding('utf-8')
	try:
	    opts,args = getopt.getopt(sys.argv[1:],"ed:a:c:")
	except getopt.GetoptError, err:
	    print str(err)
	    Usage()
	    sys.exit(2)
	if(len(opts) == 0):
		Usage();
		sys.exit(2)
	# print type(opts)
	# print opts
	# print args

	targetTID = ""
	for op,value in opts:
		if op == "-a":
			print "add TID: "+value
			targetTID = value
			addTID(value)
		if op == "-c":
			if(findTID(value)):
				print "change TID: "+value
				targetTID = value
			else:
				print "Can't find TID: "+value
				sys.exit(2)
		if op == "-d":
			if(len(targetTID) == 0):
				print "Can't find target TID.Please make sure -a or -c is before -d"
				sys.exit(2)
			print "TID: "+targetTID+"\nLang: "+defaultLang+"\ndesc: "+value
		if op == "-e":
			print "defalut Language will export"

if __name__ == '__main__':
	main(sys.argv)

# f = file(path)
# data = json.load(f)
# print type(data)
# num = 0
# for d,x in data.items():
# 	loseStr = ""
# 	# if(not x.has_key("CN")):
# 	# 	loseStr+=" CN"
# 	if(not x.has_key("EN") and x.has_key("RU")):
# 		loseStr+=" EN"
# 	# if(not x.has_key("RU")):
# 	# 	loseStr+=" RU"
# 	if(len(loseStr) > 0):
# 		print "Name is: "+d+" lose is: "+loseStr
# 		num = num+1
# print "total num is:"+str(num)