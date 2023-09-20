using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AdaptivePerformance;

public class APManager : MonoBehaviour
{
    private IAdaptivePerformance ap = null;

    private void Start()
    {
        ap = Holder.Instance;

        if(ap.Active == false)
        {
            return;
        }

        QualitySettings.lodBias = 1.0f;
        ap.ThermalStatus.ThermalEvent += OnThermalEvent;
    }

    void OnThermalEvent(ThermalMetrics ev)
    {
        switch (ev.WarningLevel)
        {
            case WarningLevel.NoWarning:
                QualitySettings.lodBias = 1;
                break;
            case WarningLevel.ThrottlingImminent:
                if(ev.TemperatureLevel > 0.8f)
                {
                    QualitySettings.lodBias = 0.75f;
                }
                else
                {
                    QualitySettings.lodBias = 1.0f;
                }
                break;
            case WarningLevel.Throttling:
                QualitySettings.lodBias = 0.5f;
                break;
        }
    }

    public void EnterMenu()
    {
        if(ap.Active == false)
        {
            return;
        }

        Application.targetFrameRate = 30;

        var ctrl = ap.DevicePerformanceControl;
        ctrl.AutomaticPerformanceControl = true;
    }

    public void EnterBenchmark()
    {
        var ctrl = ap.DevicePerformanceControl;
        ctrl.CpuLevel = ctrl.MaxCpuPerformanceLevel;
        ctrl.GpuLevel = ctrl.MaxGpuPerformanceLevel;
    }
}
