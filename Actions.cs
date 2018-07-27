
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
class APIResponse
{
	public System.Net.HttpStatusCode Code { get; set; }
	public string TextResponse { get; set; }
	public string ErrorMessage { get; set; }
	public System.Drawing.Image Image { get; set; }
}

class ControllerAction
{

	public ControllerAction(Type ExpectedResult)
	{
		ID = Guid.NewGuid();
        this.UseGrid = true;
        this.ExpectantClass = ExpectedResult;
	}

	public enum ActionResultEnum
	{
		SingleClassInstance = 1,
		ClassList = 2,
		ResponseMessage = 3,
		EskimoImage = 4
	}

	public enum MethodEnum
	{
		eGet = 1,
		ePost = 2
	}
	public Guid ID { get; set; }
	public string Name { get; set; }
	public MethodEnum Method { get; set; }
	public Controller Controller { get; set; }
	public object Parameters { get; set; }
	public ActionResultEnum ActionResult { get; set; }
	public bool UseGrid { get; set; }

    public Type ExpectantClass { get; }
}


class Controller : ICloneable
{

	public Controller()
	{
		ID = Guid.NewGuid();
        this.Actions = new List<ControllerAction>();
	}

	public Guid ID { get; set; }
	public  List<ControllerAction>  Actions { get; set; }
	public string Name { get; set; }

	public object Clone()
	{
		return this.MemberwiseClone();
	}
}
