using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestConfig : MonoBehaviour {

	// Use this for initialization
	void Start () {
		AssetLoader.Get().LoadConfig("testConfig",this.onLoaded);

	}

	void onLoaded(string name, UnityEngine.Object obj, object callbackData)
	{
		string json = obj.ToString();
		ObjectConfig config = new ObjectConfig();
		config.initialize(json);
		object 	t1 = config.getConfig("testBool");
		string 	t2 =(string)config.getConfig("testString");
		long 	t3 = (long)config.getConfig("testLong");
		double 	t4 = (double)config.getConfig("testdouble");
		string 	t5 = (string)config.getConfig("testStringInGroup",1);
		long 	t6 = (long)config.getConfig("testLongInGroup");
		Debug.Log("the Result :\n testBool:"+t1+"\ntestString:"+t2+"\ntestLong:"+t3+"\ntestdouble:"+t4+"\ntestStringInGroup:"+t5+"\ntestIntInGroup:"+t6);
	}

}
