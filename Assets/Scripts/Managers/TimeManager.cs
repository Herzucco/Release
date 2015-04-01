using UnityEngine;
using System.Collections;

/// <summary>
/// Time manager.
/// </summary>

public class TimeManager : SingleBehaviour<TimeManager, TimeManager> 
{
	#region Variables (private)

    private bool _isPause;
    
    private bool _ModifyCurrentStep;
    private float _Step;
    private float _SpeedFactor;
    
    #endregion
    
	public float speedFactor{
		get{
			return _SpeedFactor;
		}
		set{
			_SpeedFactor = value;
		}
	}
	#region Unity event functions
    
    /// <summary>
    /// Awake this instance.
    /// </summary>
    
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _ModifyCurrentStep = false;
        _SpeedFactor       = 1.0f;
        _isPause = false ;
    }

	/// <summary>
	/// Update this instance.
	/// </summary>

    void Update()
    {
        if (_ModifyCurrentStep)
        {
            _SpeedFactor = Mathf.Lerp(_SpeedFactor, _Step, Time.deltaTime);
        }
    }
    
    #endregion

	#region Methods
     
	/// <summary>
	/// Return the manager deltaTime
	/// </summary>
	/// <returns>The time.</returns>
    
    public float DeltaTime()
    {
        if (_isPause)
            return 0.0f;

        return Time.deltaTime * _SpeedFactor;
    }

	public float FixedDeltaTime()
	{
		if (_isPause)
			return 0.0f;
		
		return Time.fixedDeltaTime * _SpeedFactor;
	}

	/// <summary>
	/// Modifies the current speed factor.
	/// </summary>
	/// <param name="step">Step.</param>

    public void ModifyCurrentSpeedFactor(float step)
    {
        if (step != 0)
            _Step = step;
        else
            return;
        if (!_ModifyCurrentStep)
            _ModifyCurrentStep = true;
    }

    /// <summary>
    /// pass variable true of false to tell script that game is paused or not.
    /// </summary>
    /// <param name="currentValue">value of game paused true or false.</param>
    private void ActionToDoByPause( bool currentValue )
    {
        _isPause = currentValue;
    }
	
    #endregion
}