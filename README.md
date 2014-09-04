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

	