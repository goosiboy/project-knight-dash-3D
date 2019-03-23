using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoggerBuilder : LoggerBuilderInterface {

    private string Context;
    private string Level;

    public LoggerBuilder(string context) {
        SetContext(context);
        SetLogLevel(Constants.SHOW_CONTEXT);
    }

    public LoggerBuilder(string context, string level) {
        SetContext(context);
        SetLogLevel(level);
    }

    /**
     * Assigns the context of the class, which will be printed to the console
     */
    private void SetContext(string context){
        Context = context;
    }

    /**
     * Assigns the level of the logging. Sets the visibility of the context in the printed string. 
     */
    private void SetLogLevel(string logLevel) {
        if(!logLevel.Equals(Constants.HIDE_CONTEXT) && !logLevel.Equals(Constants.SHOW_CONTEXT)) {
            throw new System.ArgumentException("Invalid value");
        }
        Level = logLevel;
    }

    public ContextLogger Build() {
        ContextLogger printer = new ContextLogger();
        if(!Context.Equals(null)) {
            printer.Context = Context;
            printer.Level = Level;           
        } else {
            printer.Level = Level;
        }
        return printer;
    }

}

public class ContextLogger {

    public string Context { get; set; } = null;
    public string Level { get; set; } = null;

    public void Log<T>(T arg) {
        if (!Context.Equals(null)) {
            if (Level.Equals(Constants.SHOW_CONTEXT)) {
                Debug.Log("[" + Context + "] " + arg.ToString());
            } else {
                Debug.Log(arg.ToString());
            }
        } else {
            Debug.Log(arg.ToString());
        }
    }

    public void Log<T>() {}

}
