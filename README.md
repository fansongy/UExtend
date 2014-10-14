## UExtend ##

UExtend is a extend for Unity3d.This project's goal is to create a lightweight Unity3d developmend expand which help programmer work more easy. 

# Install #
You can clone the project, and run the scene under the Example folder.
The core code is in UExtend,you can just import this folder.


# Usage #

Generally, you can describ before the core file of the model.

There are some overview:

##1. MVC Modle##

### Message: ###
    
	//regist listener
	Messenger.AddListener<string> ("testMsg",OnTestMessage);
	
	//Complete the function
	void OnTestMessage(string str)	       	{		Debug.Log ("recv Message"+str);	}
	//send message
	Messenger.Broadcast<string>("testMsg","Hi Message");
	
### Singleton: ###

	MySingleton gm =  Singleton.getInstance ("MySingleton") as MySingleton;
		
### Controller ###
Based on Abstract Factory,every controller should be implement the `OperateController`.

The `TestController` is the class which manage the all kinds of `OperateController`.It also receive the user input operate,choosing the controller to use.

## 2.Log Print ##
1.define yourself as a Logger :
	
	public static Logger fansy = new Logger("fansy");2.define logger in DEFAULT_LOG_INFOS :
    new LogInfo("fansy", true,true,true,LogLevel.Debug)3.call Log:		Log.fansy.Print("yoyo check now");		Log.fansy.LogLevelPrint("yes",LogLevel.Error);		Log.fansy.ScreenPrint("Screen"+1);
		
## 3.AssetLoader ##
Load the Asset from the `Resources` or `StreamingAssets`.
It alse include the file operation,such as: copy to Resouces, make AssetBundle.

1.You can use LoadGameObject() to load the GameObject from the AssetBundle:      	 
    AssetLoader.Get().LoadGameObject("sp1",this.onLloaded);2.Then to implement the callback:
	void onLoaded(string name, Object go, object callbackData)	{		Log.fansy.ScreenPrint ("load call back!");		Instantiate(go);	}3.You can use LoadConfig to load Object File	AssetLoader.Get().LoadConfig("log",new AssetLoader.ObjectCallback(this.onConfig));
4.Then to implement the callback

    void onConfig(string name, Object obj, object callbackData)	{		Log.fansy.ScreenPrint ("config call back!");		Log.fansy.ScreenPrint (obj.ToString());	}

## 4.Localize ##
This model is designed to make muti-language program.We can easily get the text of traget language by follow this step:

1.set current language

	Localize.Get().setCurLang(langs[curSelected]);
2.call func to get the text

	Localize.Get().getStringByTID("TID_TEST_LANGUAGE_TITLE");
		
The text is saved in config file.

In order to add TID easily, I wrote a python script to manage TID. It's named TIDMake which is in the same level with this file.It can add,change TID, and also can export one kind of language for translating. we can add a new TID by calling:

	python TIDMake.py -a TID_NEW -d "I'm descirbe"

## Useful Component #####1.CallSelfMethod ###
It can call the public function of other componet in same gameObject.It can detect the function in editor,and set the delay time of calling after start().### 2.CurveRotation ###It is an rotating animation component which is controled by curve.

	