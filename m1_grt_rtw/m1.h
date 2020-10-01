/*
 * m1.h
 *
 * Code generation for model "m1".
 *
 * Model version              : 1.8
 * Simulink Coder version : 8.13 (R2017b) 24-Jul-2017
 * C source code generated on : Mon Mar  1 18:13:04 2021
 *
 * Target selection: grt.tlc
 * Note: GRT includes extra infrastructure and instrumentation for prototyping
 * Embedded hardware selection: Intel->x86-64 (Windows64)
 * Code generation objectives: Unspecified
 * Validation result: Not run
 */

#ifndef RTW_HEADER_m1_h_
#define RTW_HEADER_m1_h_
#include <math.h>
#include <stdlib.h>
#include <string.h>
#include <float.h>
#include <stddef.h>
#ifndef m1_COMMON_INCLUDES_
# define m1_COMMON_INCLUDES_
#include "rtwtypes.h"
#include "rtw_continuous.h"
#include "rtw_solver.h"
#include "rt_logging.h"
#include "stdlib.h"
#endif                                 /* m1_COMMON_INCLUDES_ */

#include "m1_types.h"

/* Shared type includes */
#include "multiword_types.h"
#include "rt_nonfinite.h"

/* Macros for accessing real-time model data structure */
#ifndef rtmGetFinalTime
# define rtmGetFinalTime(rtm)          ((rtm)->Timing.tFinal)
#endif

#ifndef rtmGetRTWLogInfo
# define rtmGetRTWLogInfo(rtm)         ((rtm)->rtwLogInfo)
#endif

#ifndef rtmGetErrorStatus
# define rtmGetErrorStatus(rtm)        ((rtm)->errorStatus)
#endif

#ifndef rtmSetErrorStatus
# define rtmSetErrorStatus(rtm, val)   ((rtm)->errorStatus = (val))
#endif

#ifndef rtmGetStopRequested
# define rtmGetStopRequested(rtm)      ((rtm)->Timing.stopRequestedFlag)
#endif

#ifndef rtmSetStopRequested
# define rtmSetStopRequested(rtm, val) ((rtm)->Timing.stopRequestedFlag = (val))
#endif

#ifndef rtmGetStopRequestedPtr
# define rtmGetStopRequestedPtr(rtm)   (&((rtm)->Timing.stopRequestedFlag))
#endif

#ifndef rtmGetT
# define rtmGetT(rtm)                  ((rtm)->Timing.taskTime0)
#endif

#ifndef rtmGetTFinal
# define rtmGetTFinal(rtm)             ((rtm)->Timing.tFinal)
#endif

/* Block signals (auto storage) */
typedef struct {
  creal_T RealImagtoComplex1;          /* '<Root>/Real-Imag to Complex1' */
  creal_T Downsample;                  /* '<Root>/Downsample' */
  real_T ComplextoRealImag_o1;         /* '<Root>/Complex to Real-Imag' */
  real_T ComplextoRealImag_o2;         /* '<Root>/Complex to Real-Imag' */
  real_T DataTypeConversion;           /* '<Root>/Data Type Conversion' */
  real_T DataTypeConversion1;          /* '<Root>/Data Type Conversion1' */
} B_m1_T;

/* Block states (auto storage) for system '<Root>' */
typedef struct {
  real_T SineWave_AccFreqNorm;         /* '<Root>/Sine Wave' */
  real_T SineWave1_AccFreqNorm;        /* '<Root>/Sine Wave1' */
  real_T SineWave2_AccFreqNorm;        /* '<Root>/Sine Wave2' */
  real_T Buffer_CircBuf[20];           /* '<Root>/Buffer' */
  real_T Buffer1_CircBuf[20];          /* '<Root>/Buffer1' */
  real_T DigitalFilter_states[121];    /* '<S3>/Digital Filter' */
  real_T DigitalFilter_states_g[121];  /* '<S4>/Digital Filter' */
  real_T RandomSource_STATE_DWORK[35]; /* '<Root>/Random Source' */
  int64m_T filter_CICStates[8];        /* '<S5>/filter' */
  int64m_T filter_CombStateData[8];    /* '<S5>/filter' */
  int64m_T filter_CICStates_l[8];      /* '<S6>/filter' */
  int64m_T filter_CombStateData_j[8];  /* '<S6>/filter' */
  int32_T Buffer_inBufPtrIdx;          /* '<Root>/Buffer' */
  int32_T Buffer_outBufPtrIdx;         /* '<Root>/Buffer' */
  int32_T Buffer_bufferLength;         /* '<Root>/Buffer' */
  int32_T Buffer1_inBufPtrIdx;         /* '<Root>/Buffer1' */
  int32_T Buffer1_outBufPtrIdx;        /* '<Root>/Buffer1' */
  int32_T Buffer1_bufferLength;        /* '<Root>/Buffer1' */
  int32_T filter_CircBufCounter;       /* '<S5>/filter' */
  int32_T filter_CircBufCounter_p;     /* '<S6>/filter' */
  uint32_T RandomSource_SEED_DWORK;    /* '<Root>/Random Source' */
} DW_m1_T;

/* Parameters (auto storage) */
struct P_m1_T_ {
  real_T SineWave_Amplitude;           /* Mask Parameter: SineWave_Amplitude
                                        * Referenced by: '<Root>/Sine Wave'
                                        */
  real_T SineWave1_Amplitude;          /* Mask Parameter: SineWave1_Amplitude
                                        * Referenced by: '<Root>/Sine Wave1'
                                        */
  real_T SineWave2_Amplitude;          /* Mask Parameter: SineWave2_Amplitude
                                        * Referenced by: '<Root>/Sine Wave2'
                                        */
  real_T SineWave_Frequency;           /* Mask Parameter: SineWave_Frequency
                                        * Referenced by: '<Root>/Sine Wave'
                                        */
  real_T SineWave1_Frequency;          /* Mask Parameter: SineWave1_Frequency
                                        * Referenced by: '<Root>/Sine Wave1'
                                        */
  real_T SineWave2_Frequency;          /* Mask Parameter: SineWave2_Frequency
                                        * Referenced by: '<Root>/Sine Wave2'
                                        */
  real_T RandomSource_MaxVal;          /* Mask Parameter: RandomSource_MaxVal
                                        * Referenced by: '<Root>/Random Source'
                                        */
  real_T RandomSource_MinVal;          /* Mask Parameter: RandomSource_MinVal
                                        * Referenced by: '<Root>/Random Source'
                                        */
  real_T SineWave_Phase;               /* Mask Parameter: SineWave_Phase
                                        * Referenced by: '<Root>/Sine Wave'
                                        */
  real_T SineWave1_Phase;              /* Mask Parameter: SineWave1_Phase
                                        * Referenced by: '<Root>/Sine Wave1'
                                        */
  real_T SineWave2_Phase;              /* Mask Parameter: SineWave2_Phase
                                        * Referenced by: '<Root>/Sine Wave2'
                                        */
  real_T Buffer_ic;                    /* Expression: 0
                                        * Referenced by: '<Root>/Buffer'
                                        */
  real_T Buffer1_ic;                   /* Expression: 0
                                        * Referenced by: '<Root>/Buffer1'
                                        */
  real_T DigitalFilter_InitialStates;  /* Expression: 0
                                        * Referenced by: '<S3>/Digital Filter'
                                        */
  real_T DigitalFilter_Coefficients[122];/* Expression: [-0.000135737057761252031 -0.000616522698721886113 -0.00140882386784273975 -0.00266821622058643904 -0.00418736772600851719 -0.00568147298467863195 -0.00668012006116477583 -0.00674150809256497327 -0.00559822100087768292 -0.00333686732218057506 -0.000447272361725657643 0.0022724433187048575 0.00396964318613426004 0.00406238254585770779 0.00251120560583869114 -9.44468218784840895e-05 -0.00270502639097230067 -0.00420159228769961118 -0.0038664145267523326 -0.00173378878460296029 0.00135393732357355374 0.00404864989604567134 0.0050623157063143685 0.00376729856004854105 0.000555985079480145173 -0.00323724781682611755 -0.00586515365042656993 -0.0059506198950682633 -0.0031818311857602353 0.00142134517754837712 0.00584616390728309645 0.00793914849016430502 0.00642411470586530704 0.00161699145647642992 -0.00454000858298090997 -0.00922158858546066884 -0.00997615027750588072 -0.00595380406922055294 0.00148598457390306882 0.009153777540210465 0.0133367349014620815 0.0115221825554122394 0.00377007216224326414 -0.00696505286477720233 -0.015851022920811083 -0.0182311632828675366 -0.0118867228828230202 0.00152097890059819029 0.0166141516226834679 0.0262219932233368133 0.0244452386201097008 0.00965511350845722933 -0.0138987809157136242 -0.0367568163898459518 -0.0472099031493300458 -0.0356138259182696357 0.00153061950681072576 0.0593678050192638501 0.125368074569479376 0.182861625279957141 0.216282578392981095 0.216282578392981095 0.182861625279957141 0.125368074569479376 0.0593678050192638501 0.00153061950681072576 -0.0356138259182696357 -0.0472099031493300458 -0.0367568163898459518 -0.0138987809157136242 0.00965511350845722933 0.0244452386201097008 0.0262219932233368133 0.0166141516226834679 0.00152097890059819029 -0.0118867228828230202 -0.0182311632828675366 -0.015851022920811083 -0.00696505286477720233 0.00377007216224326414 0.0115221825554122394 0.0133367349014620815 0.009153777540210465 0.00148598457390306882 -0.00595380406922055294 -0.00997615027750588072 -0.00922158858546066884 -0.00454000858298090997 0.00161699145647642992 0.00642411470586530704 0.00793914849016430502 0.00584616390728309645 0.00142134517754837712 -0.0031818311857602353 -0.0059506198950682633 -0.00586515365042656993 -0.00323724781682611755 0.000555985079480145173 0.00376729856004854105 0.0050623157063143685 0.00404864989604567134 0.00135393732357355374 -0.00173378878460296029 -0.0038664145267523326 -0.00420159228769961118 -0.00270502639097230067 -9.44468218784840895e-05 0.00251120560583869114 0.00406238254585770779 0.00396964318613426004 0.0022724433187048575 -0.000447272361725657643 -0.00333686732218057506 -0.00559822100087768292 -0.00674150809256497327 -0.00668012006116477583 -0.00568147298467863195 -0.00418736772600851719 -0.00266821622058643904 -0.00140882386784273975 -0.000616522698721886113 -0.000135737057761252031]
                                          * Referenced by: '<S3>/Digital Filter'
                                          */
  real_T DigitalFilter_InitialStates_k;/* Expression: 0
                                        * Referenced by: '<S4>/Digital Filter'
                                        */
  real_T DigitalFilter_Coefficients_f[122];/* Expression: [-0.000135737057761252031 -0.000616522698721886113 -0.00140882386784273975 -0.00266821622058643904 -0.00418736772600851719 -0.00568147298467863195 -0.00668012006116477583 -0.00674150809256497327 -0.00559822100087768292 -0.00333686732218057506 -0.000447272361725657643 0.0022724433187048575 0.00396964318613426004 0.00406238254585770779 0.00251120560583869114 -9.44468218784840895e-05 -0.00270502639097230067 -0.00420159228769961118 -0.0038664145267523326 -0.00173378878460296029 0.00135393732357355374 0.00404864989604567134 0.0050623157063143685 0.00376729856004854105 0.000555985079480145173 -0.00323724781682611755 -0.00586515365042656993 -0.0059506198950682633 -0.0031818311857602353 0.00142134517754837712 0.00584616390728309645 0.00793914849016430502 0.00642411470586530704 0.00161699145647642992 -0.00454000858298090997 -0.00922158858546066884 -0.00997615027750588072 -0.00595380406922055294 0.00148598457390306882 0.009153777540210465 0.0133367349014620815 0.0115221825554122394 0.00377007216224326414 -0.00696505286477720233 -0.015851022920811083 -0.0182311632828675366 -0.0118867228828230202 0.00152097890059819029 0.0166141516226834679 0.0262219932233368133 0.0244452386201097008 0.00965511350845722933 -0.0138987809157136242 -0.0367568163898459518 -0.0472099031493300458 -0.0356138259182696357 0.00153061950681072576 0.0593678050192638501 0.125368074569479376 0.182861625279957141 0.216282578392981095 0.216282578392981095 0.182861625279957141 0.125368074569479376 0.0593678050192638501 0.00153061950681072576 -0.0356138259182696357 -0.0472099031493300458 -0.0367568163898459518 -0.0138987809157136242 0.00965511350845722933 0.0244452386201097008 0.0262219932233368133 0.0166141516226834679 0.00152097890059819029 -0.0118867228828230202 -0.0182311632828675366 -0.015851022920811083 -0.00696505286477720233 0.00377007216224326414 0.0115221825554122394 0.0133367349014620815 0.009153777540210465 0.00148598457390306882 -0.00595380406922055294 -0.00997615027750588072 -0.00922158858546066884 -0.00454000858298090997 0.00161699145647642992 0.00642411470586530704 0.00793914849016430502 0.00584616390728309645 0.00142134517754837712 -0.0031818311857602353 -0.0059506198950682633 -0.00586515365042656993 -0.00323724781682611755 0.000555985079480145173 0.00376729856004854105 0.0050623157063143685 0.00404864989604567134 0.00135393732357355374 -0.00173378878460296029 -0.0038664145267523326 -0.00420159228769961118 -0.00270502639097230067 -9.44468218784840895e-05 0.00251120560583869114 0.00406238254585770779 0.00396964318613426004 0.0022724433187048575 -0.000447272361725657643 -0.00333686732218057506 -0.00559822100087768292 -0.00674150809256497327 -0.00668012006116477583 -0.00568147298467863195 -0.00418736772600851719 -0.00266821622058643904 -0.00140882386784273975 -0.000616522698721886113 -0.000135737057761252031]
                                            * Referenced by: '<S4>/Digital Filter'
                                            */
};

/* Real-time Model Data Structure */
struct tag_RTM_m1_T {
  const char_T *errorStatus;
  RTWLogInfo *rtwLogInfo;

  /*
   * Timing:
   * The following substructure contains information regarding
   * the timing information for the model.
   */
  struct {
    time_T taskTime0;
    uint32_T clockTick0;
    uint32_T clockTickH0;
    time_T stepSize0;
    struct {
      uint8_T TID[3];
    } TaskCounters;

    time_T tFinal;
    boolean_T stopRequestedFlag;
  } Timing;
};

/* Block parameters (auto storage) */
extern P_m1_T m1_P;

/* Block signals (auto storage) */
extern B_m1_T m1_B;

/* Block states (auto storage) */
extern DW_m1_T m1_DW;

/* Model entry point functions */
extern void m1_initialize(void);
extern void m1_step(void);
extern void m1_terminate(void);

/* Real-time Model object */
extern RT_MODEL_m1_T *const m1_M;

/*-
 * The generated code includes comments that allow you to trace directly
 * back to the appropriate location in the model.  The basic format
 * is <system>/block_name, where system is the system number (uniquely
 * assigned by Simulink) and block_name is the name of the block.
 *
 * Use the MATLAB hilite_system command to trace the generated code back
 * to the model.  For example,
 *
 * hilite_system('<S3>')    - opens system 3
 * hilite_system('<S3>/Kp') - opens and selects block Kp which resides in S3
 *
 * Here is the system hierarchy for this model
 *
 * '<Root>' : 'm1'
 * '<S1>'   : 'm1/CIC Filter'
 * '<S2>'   : 'm1/CIC Filter1'
 * '<S3>'   : 'm1/Digital Filter Design'
 * '<S4>'   : 'm1/Digital Filter Design1'
 * '<S5>'   : 'm1/CIC Filter/Generated Filter Block'
 * '<S6>'   : 'm1/CIC Filter1/Generated Filter Block'
 * '<S7>'   : 'm1/Digital Filter Design/Check Signal Attributes'
 * '<S8>'   : 'm1/Digital Filter Design1/Check Signal Attributes'
 */
#endif                                 /* RTW_HEADER_m1_h_ */
