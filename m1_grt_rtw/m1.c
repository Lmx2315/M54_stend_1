/*
 * m1.c
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

#include "m1.h"
#include "m1_private.h"

/* Block signals (auto storage) */
B_m1_T m1_B;

/* Block states (auto storage) */
DW_m1_T m1_DW;

/* Real-time model */
RT_MODEL_m1_T m1_M_;
RT_MODEL_m1_T *const m1_M = &m1_M_;
static void rate_scheduler(void);
void MultiWordSignedWrap(const uint32_T u1[], int32_T n1, uint32_T n2, uint32_T
  y[])
{
  int32_T n1m1;
  int32_T i;
  uint32_T mask;
  uint32_T ys;
  n1m1 = n1 - 1;
  for (i = 0; i < n1m1; i++) {
    y[i] = u1[i];
  }

  mask = 1U << (31U - n2);
  ys = (u1[n1m1] & mask) != 0U ? MAX_uint32_T : 0U;
  mask = (mask << 1U) - 1U;
  y[n1m1] = (u1[n1m1] & mask) | (~mask & ys);
}

void MultiWordAdd(const uint32_T u1[], const uint32_T u2[], uint32_T y[],
                  int32_T n)
{
  uint32_T yi;
  uint32_T u1i;
  uint32_T carry = 0U;
  int32_T i;
  for (i = 0; i < n; i++) {
    u1i = u1[i];
    yi = (u1i + u2[i]) + carry;
    y[i] = yi;
    carry = carry != 0U ? (uint32_T)(yi <= u1i) : (uint32_T)(yi < u1i);
  }
}

void sLong2MultiWord(int32_T u, uint32_T y[], int32_T n)
{
  uint32_T yi;
  int32_T i;
  y[0] = (uint32_T)u;
  yi = u < 0 ? MAX_uint32_T : 0U;
  for (i = 1; i < n; i++) {
    y[i] = yi;
  }
}

void MultiWordSub(const uint32_T u1[], const uint32_T u2[], uint32_T y[],
                  int32_T n)
{
  uint32_T yi;
  uint32_T u1i;
  uint32_T borrow = 0U;
  int32_T i;
  for (i = 0; i < n; i++) {
    u1i = u1[i];
    yi = (u1i - u2[i]) - borrow;
    y[i] = yi;
    borrow = borrow != 0U ? (uint32_T)(yi >= u1i) : (uint32_T)(yi > u1i);
  }
}

real_T sMultiWord2Double(const uint32_T u1[], int32_T n1, int32_T e1)
{
  real_T y;
  int32_T i;
  int32_T exp_0;
  uint32_T u1i;
  uint32_T cb;
  y = 0.0;
  exp_0 = e1;
  if ((u1[n1 - 1] & 2147483648U) != 0U) {
    cb = 1U;
    for (i = 0; i < n1; i++) {
      u1i = ~u1[i];
      cb += u1i;
      y -= ldexp(cb, exp_0);
      cb = (uint32_T)(cb < u1i);
      exp_0 += 32;
    }
  } else {
    for (i = 0; i < n1; i++) {
      y += ldexp(u1[i], exp_0);
      exp_0 += 32;
    }
  }

  return y;
}

/*
 *   This function updates active task flag for each subrate.
 * The function is called at model base rate, hence the
 * generated code self-manages all its subrates.
 */
static void rate_scheduler(void)
{
  /* Compute which subrates run during the next base time step.  Subrates
   * are an integer multiple of the base rate counter.  Therefore, the subtask
   * counter is reset when it reaches its limit (zero means run).
   */
  (m1_M->Timing.TaskCounters.TID[1])++;
  if ((m1_M->Timing.TaskCounters.TID[1]) > 9) {/* Sample time: [0.041666666666666664s, 0.0s] */
    m1_M->Timing.TaskCounters.TID[1] = 0;
  }

  (m1_M->Timing.TaskCounters.TID[2])++;
  if ((m1_M->Timing.TaskCounters.TID[2]) > 19) {/* Sample time: [0.083333333333333329s, 0.0s] */
    m1_M->Timing.TaskCounters.TID[2] = 0;
  }
}

void RandSrcInitState_U_64(const uint32_T seed[], real_T state[], int32_T nChans)
{
  int32_T i;
  uint32_T j;
  int32_T k;
  int32_T n;
  real_T d;

  /* Start for S-Function (sdsprandsrc2): '<Root>/Random Source' */
  /* RandSrcInitState_U_64 */
  for (i = 0; i < nChans; i++) {
    j = seed[i] != 0U ? seed[i] : 2147483648U;
    state[35 * i + 34] = j;
    for (k = 0; k < 32; k++) {
      d = 0.0;
      for (n = 0; n < 53; n++) {
        j ^= j << 13;
        j ^= j >> 17;
        j ^= j << 5;
        d = (real_T)((int32_T)(j >> 19) & 1) + (d + d);
      }

      state[35 * i + k] = ldexp(d, -53);
    }

    state[35 * i + 32] = 0.0;
    state[35 * i + 33] = 0.0;
  }

  /* End of Start for S-Function (sdsprandsrc2): '<Root>/Random Source' */
}

void RandSrc_U_D(real_T y[], const real_T minVec[], int32_T minLen, const real_T
                 maxVec[], int32_T maxLen, real_T state[], int32_T nChans,
                 int32_T nSamps)
{
  int32_T one;
  int8_T *onePtr;
  int32_T chan;
  real_T min;
  real_T max;
  int32_T samps;
  real_T d;
  int32_T i;
  uint32_T j;
  int32_T ii[2];
  int32_T i_tmp;
  int32_T j_tmp;
  int32_T d_tmp;

  /* Start for S-Function (sdsprandsrc2): '<Root>/Random Source' */
  /* RandSrc_U_D */
  one = 1;
  onePtr = (int8_T *)&one;
  one = (onePtr[0U] == 0);
  for (chan = 0; chan < nChans; chan++) {
    min = minVec[minLen > 1 ? chan : 0];
    max = maxVec[maxLen > 1 ? chan : 0];
    max -= min;
    i_tmp = chan * 35 + 33;
    i = (int32_T)((uint32_T)state[i_tmp] & 31U);
    j_tmp = chan * 35 + 34;
    j = (uint32_T)state[j_tmp];
    for (samps = 0; samps < nSamps; samps++) {
      /* "Subtract with borrow" generator */
      d = state[((i + 20) & 31) + chan * 35];
      d -= state[((i + 5) & 31) + chan * 35];
      d_tmp = chan * 35 + 32;
      d -= state[d_tmp];
      if (d >= 0.0) {
        state[d_tmp] = 0.0;
      } else {
        d++;

        /* set 1 in LSB */
        state[d_tmp] = 1.1102230246251565E-16;
      }

      state[chan * 35 + i] = d;
      i = (i + 1) & 31;

      /* XOR with shift register sequence */
      memcpy(&ii[0U], &d, sizeof(real_T));
      ii[one] ^= j;
      j ^= j << 13;
      j ^= j >> 17;
      j ^= j << 5;
      ii[one ^ 1] ^= j & 1048575U;
      memcpy(&d, &ii[0U], sizeof(real_T));
      y[chan * nSamps + samps] = max * d + min;
    }

    state[i_tmp] = i;
    state[j_tmp] = j;
  }

  /* End of Start for S-Function (sdsprandsrc2): '<Root>/Random Source' */
}

void RandSrcCreateSeeds_64(uint32_T initSeed, uint32_T seedArray[], int32_T
  numSeeds)
{
  real_T state[35];
  real_T tmp;
  real_T min;
  real_T max;
  int32_T i;

  /* Start for S-Function (sdsprandsrc2): '<Root>/Random Source' */
  /* RandSrcCreateSeeds_64 */
  min = 0.0;
  max = 1.0;
  RandSrcInitState_U_64(&initSeed, state, 1);
  for (i = 0; i < numSeeds; i++) {
    RandSrc_U_D(&tmp, &min, 1, &max, 1, state, 1, 1);
    seedArray[i] = (uint32_T)(tmp * 2.147483648E+9);
  }

  /* End of Start for S-Function (sdsprandsrc2): '<Root>/Random Source' */
}

real_T rt_roundd_snf(real_T u)
{
  real_T y;
  if (fabs(u) < 4.503599627370496E+15) {
    if (u >= 0.5) {
      y = floor(u + 0.5);
    } else if (u > -0.5) {
      y = u * 0.0;
    } else {
      y = ceil(u - 0.5);
    }
  } else {
    y = u;
  }

  return y;
}

/* Model step function */
void m1_step(void)
{
  int32_T i;
  int32_T j;
  int32_T uyIdx;
  int32_T currentOffset;
  int64m_T tmpIntStage;
  int32_T sampCount;
  int64m_T tmpIntStageCurr;
  int32_T curDWorkIdx;
  int16_T rtb_InputQuantizer[10];
  int64m_T rtb_filter_h;
  int64m_T rtb_filter;
  real_T rtb_Buffer1[10];
  real_T rtb_Buffer[10];
  real_T rtb_Add;
  real_T rtb_SineWave1;
  real_T rtb_SineWave2;
  int64m_T tmp;
  int64m_T tmp_0;
  int64m_T tmp_1;
  int64m_T tmp_2;
  int64m_T tmp_3;
  int64m_T tmp_4;

  /* S-Function (sdsprandsrc2): '<Root>/Random Source' */
  RandSrc_U_D(&rtb_Add, &m1_P.RandomSource_MinVal, 1, &m1_P.RandomSource_MaxVal,
              1, m1_DW.RandomSource_STATE_DWORK, 1, 1);

  /* S-Function (sdspsine2): '<Root>/Sine Wave' */
  rtb_SineWave1 = m1_P.SineWave_Amplitude * sin(m1_DW.SineWave_AccFreqNorm);

  /* Update accumulated normalized freq value
     for next sample.  Keep in range [0 2*pi) */
  m1_DW.SineWave_AccFreqNorm += m1_P.SineWave_Frequency * 0.026179938779914941;
  if (m1_DW.SineWave_AccFreqNorm >= 6.2831853071795862) {
    m1_DW.SineWave_AccFreqNorm -= 6.2831853071795862;
  } else {
    if (m1_DW.SineWave_AccFreqNorm < 0.0) {
      m1_DW.SineWave_AccFreqNorm += 6.2831853071795862;
    }
  }

  /* End of S-Function (sdspsine2): '<Root>/Sine Wave' */

  /* Sum: '<Root>/Add' */
  rtb_Add += rtb_SineWave1;

  /* S-Function (sdspsine2): '<Root>/Sine Wave1' */
  rtb_SineWave1 = m1_P.SineWave1_Amplitude * sin(m1_DW.SineWave1_AccFreqNorm);

  /* Update accumulated normalized freq value
     for next sample.  Keep in range [0 2*pi) */
  m1_DW.SineWave1_AccFreqNorm += m1_P.SineWave1_Frequency * 0.026179938779914941;
  if (m1_DW.SineWave1_AccFreqNorm >= 6.2831853071795862) {
    m1_DW.SineWave1_AccFreqNorm -= 6.2831853071795862;
  } else {
    if (m1_DW.SineWave1_AccFreqNorm < 0.0) {
      m1_DW.SineWave1_AccFreqNorm += 6.2831853071795862;
    }
  }

  /* End of S-Function (sdspsine2): '<Root>/Sine Wave1' */

  /* S-Function (sdspsine2): '<Root>/Sine Wave2' */
  rtb_SineWave2 = m1_P.SineWave2_Amplitude * sin(m1_DW.SineWave2_AccFreqNorm);

  /* Update accumulated normalized freq value
     for next sample.  Keep in range [0 2*pi) */
  m1_DW.SineWave2_AccFreqNorm += m1_P.SineWave2_Frequency * 0.026179938779914941;
  if (m1_DW.SineWave2_AccFreqNorm >= 6.2831853071795862) {
    m1_DW.SineWave2_AccFreqNorm -= 6.2831853071795862;
  } else {
    if (m1_DW.SineWave2_AccFreqNorm < 0.0) {
      m1_DW.SineWave2_AccFreqNorm += 6.2831853071795862;
    }
  }

  /* End of S-Function (sdspsine2): '<Root>/Sine Wave2' */

  /* ComplexToRealImag: '<Root>/Complex to Real-Imag' incorporates:
   *  Product: '<Root>/Product'
   *  RealImagToComplex: '<Root>/Real-Imag to Complex'
   */
  m1_B.ComplextoRealImag_o1 = rtb_Add * rtb_SineWave1;
  m1_B.ComplextoRealImag_o2 = rtb_Add * rtb_SineWave2;

  /* Buffer: '<Root>/Buffer' */
  j = 0;
  sampCount = 20;
  uyIdx = m1_DW.Buffer_inBufPtrIdx;
  if (20 - m1_DW.Buffer_inBufPtrIdx <= 1) {
    for (i = 0; i < 20 - m1_DW.Buffer_inBufPtrIdx; i++) {
      m1_DW.Buffer_CircBuf[m1_DW.Buffer_inBufPtrIdx + i] =
        m1_B.ComplextoRealImag_o1;
    }

    uyIdx = 0;
    sampCount = m1_DW.Buffer_inBufPtrIdx;
  }

  for (i = 0; i < sampCount - 19; i++) {
    m1_DW.Buffer_CircBuf[uyIdx + i] = m1_B.ComplextoRealImag_o1;
  }

  m1_DW.Buffer_inBufPtrIdx++;
  if (m1_DW.Buffer_inBufPtrIdx >= 20) {
    m1_DW.Buffer_inBufPtrIdx -= 20;
  }

  m1_DW.Buffer_bufferLength++;
  if (m1_DW.Buffer_bufferLength > 20) {
    m1_DW.Buffer_outBufPtrIdx = (m1_DW.Buffer_outBufPtrIdx +
      m1_DW.Buffer_bufferLength) - 20;
    if (m1_DW.Buffer_outBufPtrIdx > 20) {
      m1_DW.Buffer_outBufPtrIdx -= 20;
    }

    m1_DW.Buffer_bufferLength = 20;
  }

  if (m1_M->Timing.TaskCounters.TID[1] == 0) {
    m1_DW.Buffer_bufferLength -= 10;
    if (m1_DW.Buffer_bufferLength < 0) {
      m1_DW.Buffer_outBufPtrIdx += m1_DW.Buffer_bufferLength;
      if (m1_DW.Buffer_outBufPtrIdx < 0) {
        m1_DW.Buffer_outBufPtrIdx += 20;
      }

      m1_DW.Buffer_bufferLength = 0;
    }

    uyIdx = 0;
    currentOffset = m1_DW.Buffer_outBufPtrIdx;
    if (m1_DW.Buffer_outBufPtrIdx < 0) {
      currentOffset = m1_DW.Buffer_outBufPtrIdx + 20;
    }

    curDWorkIdx = 20 - currentOffset;
    sampCount = 10;
    if (20 - currentOffset <= 10) {
      for (i = 0; i < 20 - currentOffset; i++) {
        rtb_Buffer[i] = m1_DW.Buffer_CircBuf[currentOffset + i];
      }

      uyIdx = 20 - currentOffset;
      currentOffset = 0;
      sampCount = 10 - curDWorkIdx;
    }

    for (i = 0; i < sampCount; i++) {
      rtb_Buffer[uyIdx + i] = m1_DW.Buffer_CircBuf[currentOffset + i];
    }

    m1_DW.Buffer_outBufPtrIdx = currentOffset + sampCount;
  }

  /* End of Buffer: '<Root>/Buffer' */

  /* Buffer: '<Root>/Buffer1' */
  sampCount = 20;
  uyIdx = m1_DW.Buffer1_inBufPtrIdx;
  if (20 - m1_DW.Buffer1_inBufPtrIdx <= 1) {
    for (i = 0; i < 20 - m1_DW.Buffer1_inBufPtrIdx; i++) {
      m1_DW.Buffer1_CircBuf[m1_DW.Buffer1_inBufPtrIdx + i] =
        m1_B.ComplextoRealImag_o2;
    }

    uyIdx = 0;
    sampCount = m1_DW.Buffer1_inBufPtrIdx;
  }

  for (i = 0; i < sampCount - 19; i++) {
    m1_DW.Buffer1_CircBuf[uyIdx + i] = m1_B.ComplextoRealImag_o2;
  }

  m1_DW.Buffer1_inBufPtrIdx++;
  if (m1_DW.Buffer1_inBufPtrIdx >= 20) {
    m1_DW.Buffer1_inBufPtrIdx -= 20;
  }

  m1_DW.Buffer1_bufferLength++;
  if (m1_DW.Buffer1_bufferLength > 20) {
    m1_DW.Buffer1_outBufPtrIdx = (m1_DW.Buffer1_outBufPtrIdx +
      m1_DW.Buffer1_bufferLength) - 20;
    if (m1_DW.Buffer1_outBufPtrIdx > 20) {
      m1_DW.Buffer1_outBufPtrIdx -= 20;
    }

    m1_DW.Buffer1_bufferLength = 20;
  }

  if (m1_M->Timing.TaskCounters.TID[1] == 0) {
    m1_DW.Buffer1_bufferLength -= 10;
    if (m1_DW.Buffer1_bufferLength < 0) {
      m1_DW.Buffer1_outBufPtrIdx += m1_DW.Buffer1_bufferLength;
      if (m1_DW.Buffer1_outBufPtrIdx < 0) {
        m1_DW.Buffer1_outBufPtrIdx += 20;
      }

      m1_DW.Buffer1_bufferLength = 0;
    }

    uyIdx = 0;
    currentOffset = m1_DW.Buffer1_outBufPtrIdx;
    if (m1_DW.Buffer1_outBufPtrIdx < 0) {
      currentOffset = m1_DW.Buffer1_outBufPtrIdx + 20;
    }

    curDWorkIdx = 20 - currentOffset;
    sampCount = 10;
    if (20 - currentOffset <= 10) {
      for (i = 0; i < 20 - currentOffset; i++) {
        rtb_Buffer1[i] = m1_DW.Buffer1_CircBuf[currentOffset + i];
      }

      uyIdx = 20 - currentOffset;
      currentOffset = 0;
      sampCount = 10 - curDWorkIdx;
    }

    for (i = 0; i < sampCount; i++) {
      rtb_Buffer1[uyIdx + i] = m1_DW.Buffer1_CircBuf[currentOffset + i];
    }

    m1_DW.Buffer1_outBufPtrIdx = currentOffset + sampCount;

    /* DataTypeConversion: '<S5>/Input Quantizer' */
    for (i = 0; i < 10; i++) {
      rtb_Add = rt_roundd_snf(rtb_Buffer[i] * 32768.0);
      if (rtb_Add < 32768.0) {
        if (rtb_Add >= -32768.0) {
          rtb_InputQuantizer[i] = (int16_T)rtb_Add;
        } else {
          rtb_InputQuantizer[i] = MIN_int16_T;
        }
      } else {
        rtb_InputQuantizer[i] = MAX_int16_T;
      }
    }

    /* End of DataTypeConversion: '<S5>/Input Quantizer' */

    /* S-Function (sdspcicfilter): '<S5>/filter' */
    i = 0;
    for (sampCount = 0; sampCount < 10; sampCount++) {
      /* Integrator Stage 0
       */
      tmpIntStage = m1_DW.filter_CICStates[0];
      tmp_0 = m1_DW.filter_CICStates[0];
      sLong2MultiWord(rtb_InputQuantizer[i], &tmp_2.chunks[0U], 2);
      MultiWordSignedWrap(&tmp_2.chunks[0U], 2, 21U, &tmp_1.chunks[0U]);
      MultiWordAdd(&tmp_0.chunks[0U], &tmp_1.chunks[0U], &tmp.chunks[0U], 2);
      MultiWordSignedWrap(&tmp.chunks[0U], 2, 21U, &tmp_0.chunks[0U]);
      m1_DW.filter_CICStates[0] = tmp_0;
      i++;

      /* Integrator Stage 1
       */
      tmpIntStageCurr = m1_DW.filter_CICStates[1];
      tmp_1 = m1_DW.filter_CICStates[1];
      MultiWordAdd(&tmp_1.chunks[0U], &tmpIntStage.chunks[0U], &tmp.chunks[0U],
                   2);
      MultiWordSignedWrap(&tmp.chunks[0U], 2, 21U, &tmp_1.chunks[0U]);
      m1_DW.filter_CICStates[1] = tmp_1;

      /* Integrator Stage 2
       */
      tmpIntStage = m1_DW.filter_CICStates[2];
      tmp_2 = m1_DW.filter_CICStates[2];
      MultiWordAdd(&tmp_2.chunks[0U], &tmpIntStageCurr.chunks[0U], &tmp.chunks
                   [0U], 2);
      MultiWordSignedWrap(&tmp.chunks[0U], 2, 21U, &tmp_2.chunks[0U]);
      m1_DW.filter_CICStates[2] = tmp_2;

      /* Integrator Stage 3
       */
      tmpIntStageCurr = m1_DW.filter_CICStates[3];
      tmp_0 = m1_DW.filter_CICStates[3];
      MultiWordAdd(&tmp_0.chunks[0U], &tmpIntStage.chunks[0U], &tmp.chunks[0U],
                   2);
      MultiWordSignedWrap(&tmp.chunks[0U], 2, 21U, &tmp_0.chunks[0U]);
      m1_DW.filter_CICStates[3] = tmp_0;

      /* Integrator Stage 4
       */
      tmpIntStage = m1_DW.filter_CICStates[4];
      tmp_0 = m1_DW.filter_CICStates[4];
      MultiWordAdd(&tmp_0.chunks[0U], &tmpIntStageCurr.chunks[0U], &tmp.chunks
                   [0U], 2);
      MultiWordSignedWrap(&tmp.chunks[0U], 2, 21U, &tmp_0.chunks[0U]);
      m1_DW.filter_CICStates[4] = tmp_0;

      /* Integrator Stage 5
       */
      tmpIntStageCurr = m1_DW.filter_CICStates[5];
      tmp_0 = m1_DW.filter_CICStates[5];
      MultiWordAdd(&tmp_0.chunks[0U], &tmpIntStage.chunks[0U], &tmp.chunks[0U],
                   2);
      MultiWordSignedWrap(&tmp.chunks[0U], 2, 21U, &tmp_0.chunks[0U]);
      m1_DW.filter_CICStates[5] = tmp_0;

      /* Integrator Stage 6
       */
      tmpIntStage = m1_DW.filter_CICStates[6];
      tmp_0 = m1_DW.filter_CICStates[6];
      MultiWordAdd(&tmp_0.chunks[0U], &tmpIntStageCurr.chunks[0U], &tmp.chunks
                   [0U], 2);
      MultiWordSignedWrap(&tmp.chunks[0U], 2, 21U, &tmp_0.chunks[0U]);
      m1_DW.filter_CICStates[6] = tmp_0;

      /* Integrator Stage 7
       */
      tmpIntStageCurr = m1_DW.filter_CICStates[7];
      tmp_0 = m1_DW.filter_CICStates[7];
      MultiWordAdd(&tmp_0.chunks[0U], &tmpIntStage.chunks[0U], &tmp.chunks[0U],
                   2);
      MultiWordSignedWrap(&tmp.chunks[0U], 2, 21U, &tmp_0.chunks[0U]);
      m1_DW.filter_CICStates[7] = tmp_0;
      if (sampCount == 0) {
        /*  Run down sampler at proper sample
         */
        /* Comb Filter Stage 0
         */
        tmpIntStage = m1_DW.filter_CombStateData[m1_DW.filter_CircBufCounter];
        m1_DW.filter_CombStateData[m1_DW.filter_CircBufCounter] =
          tmpIntStageCurr;
        MultiWordSub(&tmpIntStageCurr.chunks[0U], &tmpIntStage.chunks[0U],
                     &tmp.chunks[0U], 2);
        MultiWordSignedWrap(&tmp.chunks[0U], 2, 21U, &tmpIntStage.chunks[0U]);

        /* Comb Filter Stage 1
         */
        curDWorkIdx = m1_DW.filter_CircBufCounter + 1;
        tmpIntStageCurr = m1_DW.filter_CombStateData[curDWorkIdx];
        m1_DW.filter_CombStateData[curDWorkIdx] = tmpIntStage;
        MultiWordSub(&tmpIntStage.chunks[0U], &tmpIntStageCurr.chunks[0U],
                     &tmp.chunks[0U], 2);
        MultiWordSignedWrap(&tmp.chunks[0U], 2, 21U, &tmpIntStage.chunks[0U]);

        /* Comb Filter Stage 2
         */
        curDWorkIdx = m1_DW.filter_CircBufCounter + 2;
        tmpIntStageCurr = m1_DW.filter_CombStateData[curDWorkIdx];
        m1_DW.filter_CombStateData[curDWorkIdx] = tmpIntStage;
        MultiWordSub(&tmpIntStage.chunks[0U], &tmpIntStageCurr.chunks[0U],
                     &tmp.chunks[0U], 2);
        MultiWordSignedWrap(&tmp.chunks[0U], 2, 21U, &tmpIntStage.chunks[0U]);

        /* Comb Filter Stage 3
         */
        curDWorkIdx = m1_DW.filter_CircBufCounter + 3;
        tmpIntStageCurr = m1_DW.filter_CombStateData[curDWorkIdx];
        m1_DW.filter_CombStateData[curDWorkIdx] = tmpIntStage;
        MultiWordSub(&tmpIntStage.chunks[0U], &tmpIntStageCurr.chunks[0U],
                     &tmp.chunks[0U], 2);
        MultiWordSignedWrap(&tmp.chunks[0U], 2, 21U, &tmpIntStage.chunks[0U]);

        /* Comb Filter Stage 4
         */
        curDWorkIdx = m1_DW.filter_CircBufCounter + 4;
        tmpIntStageCurr = m1_DW.filter_CombStateData[curDWorkIdx];
        m1_DW.filter_CombStateData[curDWorkIdx] = tmpIntStage;
        MultiWordSub(&tmpIntStage.chunks[0U], &tmpIntStageCurr.chunks[0U],
                     &tmp.chunks[0U], 2);
        MultiWordSignedWrap(&tmp.chunks[0U], 2, 21U, &tmpIntStage.chunks[0U]);

        /* Comb Filter Stage 5
         */
        curDWorkIdx = m1_DW.filter_CircBufCounter + 5;
        tmpIntStageCurr = m1_DW.filter_CombStateData[curDWorkIdx];
        m1_DW.filter_CombStateData[curDWorkIdx] = tmpIntStage;
        MultiWordSub(&tmpIntStage.chunks[0U], &tmpIntStageCurr.chunks[0U],
                     &tmp.chunks[0U], 2);
        MultiWordSignedWrap(&tmp.chunks[0U], 2, 21U, &tmpIntStage.chunks[0U]);

        /* Comb Filter Stage 6
         */
        curDWorkIdx = m1_DW.filter_CircBufCounter + 6;
        tmpIntStageCurr = m1_DW.filter_CombStateData[curDWorkIdx];
        m1_DW.filter_CombStateData[curDWorkIdx] = tmpIntStage;
        MultiWordSub(&tmpIntStage.chunks[0U], &tmpIntStageCurr.chunks[0U],
                     &tmp.chunks[0U], 2);
        MultiWordSignedWrap(&tmp.chunks[0U], 2, 21U, &tmpIntStage.chunks[0U]);

        /* Comb Filter Stage 7
         */
        curDWorkIdx = m1_DW.filter_CircBufCounter + 7;
        tmpIntStageCurr = m1_DW.filter_CombStateData[curDWorkIdx];
        m1_DW.filter_CombStateData[curDWorkIdx] = tmpIntStage;
        m1_DW.filter_CircBufCounter++;
        if (m1_DW.filter_CircBufCounter >= 1) {
          m1_DW.filter_CircBufCounter--;
        }

        MultiWordSub(&tmpIntStage.chunks[0U], &tmpIntStageCurr.chunks[0U],
                     &tmp.chunks[0U], 2);
        MultiWordSignedWrap(&tmp.chunks[0U], 2, 21U, &rtb_filter.chunks[0U]);
      }
    }

    /* End of S-Function (sdspcicfilter): '<S5>/filter' */

    /* DataTypeConversion: '<S6>/Input Quantizer' */
    for (i = 0; i < 10; i++) {
      rtb_Add = rt_roundd_snf(rtb_Buffer1[i] * 32768.0);
      if (rtb_Add < 32768.0) {
        if (rtb_Add >= -32768.0) {
          rtb_InputQuantizer[i] = (int16_T)rtb_Add;
        } else {
          rtb_InputQuantizer[i] = MIN_int16_T;
        }
      } else {
        rtb_InputQuantizer[i] = MAX_int16_T;
      }
    }

    /* End of DataTypeConversion: '<S6>/Input Quantizer' */

    /* S-Function (sdspcicfilter): '<S6>/filter' */
    i = 0;
    for (sampCount = 0; sampCount < 10; sampCount++) {
      /* Integrator Stage 0
       */
      tmpIntStage = m1_DW.filter_CICStates_l[0];
      tmp_0 = m1_DW.filter_CICStates_l[0];
      sLong2MultiWord(rtb_InputQuantizer[i], &tmp_4.chunks[0U], 2);
      MultiWordSignedWrap(&tmp_4.chunks[0U], 2, 21U, &tmp_3.chunks[0U]);
      MultiWordAdd(&tmp_0.chunks[0U], &tmp_3.chunks[0U], &tmp.chunks[0U], 2);
      MultiWordSignedWrap(&tmp.chunks[0U], 2, 21U, &tmp_0.chunks[0U]);
      m1_DW.filter_CICStates_l[0] = tmp_0;
      i++;

      /* Integrator Stage 1
       */
      tmpIntStageCurr = m1_DW.filter_CICStates_l[1];
      tmp_3 = m1_DW.filter_CICStates_l[1];
      MultiWordAdd(&tmp_3.chunks[0U], &tmpIntStage.chunks[0U], &tmp.chunks[0U],
                   2);
      MultiWordSignedWrap(&tmp.chunks[0U], 2, 21U, &tmp_3.chunks[0U]);
      m1_DW.filter_CICStates_l[1] = tmp_3;

      /* Integrator Stage 2
       */
      tmpIntStage = m1_DW.filter_CICStates_l[2];
      tmp_4 = m1_DW.filter_CICStates_l[2];
      MultiWordAdd(&tmp_4.chunks[0U], &tmpIntStageCurr.chunks[0U], &tmp.chunks
                   [0U], 2);
      MultiWordSignedWrap(&tmp.chunks[0U], 2, 21U, &tmp_4.chunks[0U]);
      m1_DW.filter_CICStates_l[2] = tmp_4;

      /* Integrator Stage 3
       */
      tmpIntStageCurr = m1_DW.filter_CICStates_l[3];
      tmp_0 = m1_DW.filter_CICStates_l[3];
      MultiWordAdd(&tmp_0.chunks[0U], &tmpIntStage.chunks[0U], &tmp.chunks[0U],
                   2);
      MultiWordSignedWrap(&tmp.chunks[0U], 2, 21U, &tmp_0.chunks[0U]);
      m1_DW.filter_CICStates_l[3] = tmp_0;

      /* Integrator Stage 4
       */
      tmpIntStage = m1_DW.filter_CICStates_l[4];
      tmp_0 = m1_DW.filter_CICStates_l[4];
      MultiWordAdd(&tmp_0.chunks[0U], &tmpIntStageCurr.chunks[0U], &tmp.chunks
                   [0U], 2);
      MultiWordSignedWrap(&tmp.chunks[0U], 2, 21U, &tmp_0.chunks[0U]);
      m1_DW.filter_CICStates_l[4] = tmp_0;

      /* Integrator Stage 5
       */
      tmpIntStageCurr = m1_DW.filter_CICStates_l[5];
      tmp_0 = m1_DW.filter_CICStates_l[5];
      MultiWordAdd(&tmp_0.chunks[0U], &tmpIntStage.chunks[0U], &tmp.chunks[0U],
                   2);
      MultiWordSignedWrap(&tmp.chunks[0U], 2, 21U, &tmp_0.chunks[0U]);
      m1_DW.filter_CICStates_l[5] = tmp_0;

      /* Integrator Stage 6
       */
      tmpIntStage = m1_DW.filter_CICStates_l[6];
      tmp_0 = m1_DW.filter_CICStates_l[6];
      MultiWordAdd(&tmp_0.chunks[0U], &tmpIntStageCurr.chunks[0U], &tmp.chunks
                   [0U], 2);
      MultiWordSignedWrap(&tmp.chunks[0U], 2, 21U, &tmp_0.chunks[0U]);
      m1_DW.filter_CICStates_l[6] = tmp_0;

      /* Integrator Stage 7
       */
      tmpIntStageCurr = m1_DW.filter_CICStates_l[7];
      tmp_0 = m1_DW.filter_CICStates_l[7];
      MultiWordAdd(&tmp_0.chunks[0U], &tmpIntStage.chunks[0U], &tmp.chunks[0U],
                   2);
      MultiWordSignedWrap(&tmp.chunks[0U], 2, 21U, &tmp_0.chunks[0U]);
      m1_DW.filter_CICStates_l[7] = tmp_0;
      if (sampCount == 0) {
        /*  Run down sampler at proper sample
         */
        /* Comb Filter Stage 0
         */
        tmpIntStage = m1_DW.filter_CombStateData_j[m1_DW.filter_CircBufCounter_p];
        m1_DW.filter_CombStateData_j[m1_DW.filter_CircBufCounter_p] =
          tmpIntStageCurr;
        MultiWordSub(&tmpIntStageCurr.chunks[0U], &tmpIntStage.chunks[0U],
                     &tmp.chunks[0U], 2);
        MultiWordSignedWrap(&tmp.chunks[0U], 2, 21U, &tmpIntStage.chunks[0U]);

        /* Comb Filter Stage 1
         */
        curDWorkIdx = m1_DW.filter_CircBufCounter_p + 1;
        tmpIntStageCurr = m1_DW.filter_CombStateData_j[curDWorkIdx];
        m1_DW.filter_CombStateData_j[curDWorkIdx] = tmpIntStage;
        MultiWordSub(&tmpIntStage.chunks[0U], &tmpIntStageCurr.chunks[0U],
                     &tmp.chunks[0U], 2);
        MultiWordSignedWrap(&tmp.chunks[0U], 2, 21U, &tmpIntStage.chunks[0U]);

        /* Comb Filter Stage 2
         */
        curDWorkIdx = m1_DW.filter_CircBufCounter_p + 2;
        tmpIntStageCurr = m1_DW.filter_CombStateData_j[curDWorkIdx];
        m1_DW.filter_CombStateData_j[curDWorkIdx] = tmpIntStage;
        MultiWordSub(&tmpIntStage.chunks[0U], &tmpIntStageCurr.chunks[0U],
                     &tmp.chunks[0U], 2);
        MultiWordSignedWrap(&tmp.chunks[0U], 2, 21U, &tmpIntStage.chunks[0U]);

        /* Comb Filter Stage 3
         */
        curDWorkIdx = m1_DW.filter_CircBufCounter_p + 3;
        tmpIntStageCurr = m1_DW.filter_CombStateData_j[curDWorkIdx];
        m1_DW.filter_CombStateData_j[curDWorkIdx] = tmpIntStage;
        MultiWordSub(&tmpIntStage.chunks[0U], &tmpIntStageCurr.chunks[0U],
                     &tmp.chunks[0U], 2);
        MultiWordSignedWrap(&tmp.chunks[0U], 2, 21U, &tmpIntStage.chunks[0U]);

        /* Comb Filter Stage 4
         */
        curDWorkIdx = m1_DW.filter_CircBufCounter_p + 4;
        tmpIntStageCurr = m1_DW.filter_CombStateData_j[curDWorkIdx];
        m1_DW.filter_CombStateData_j[curDWorkIdx] = tmpIntStage;
        MultiWordSub(&tmpIntStage.chunks[0U], &tmpIntStageCurr.chunks[0U],
                     &tmp.chunks[0U], 2);
        MultiWordSignedWrap(&tmp.chunks[0U], 2, 21U, &tmpIntStage.chunks[0U]);

        /* Comb Filter Stage 5
         */
        curDWorkIdx = m1_DW.filter_CircBufCounter_p + 5;
        tmpIntStageCurr = m1_DW.filter_CombStateData_j[curDWorkIdx];
        m1_DW.filter_CombStateData_j[curDWorkIdx] = tmpIntStage;
        MultiWordSub(&tmpIntStage.chunks[0U], &tmpIntStageCurr.chunks[0U],
                     &tmp.chunks[0U], 2);
        MultiWordSignedWrap(&tmp.chunks[0U], 2, 21U, &tmpIntStage.chunks[0U]);

        /* Comb Filter Stage 6
         */
        curDWorkIdx = m1_DW.filter_CircBufCounter_p + 6;
        tmpIntStageCurr = m1_DW.filter_CombStateData_j[curDWorkIdx];
        m1_DW.filter_CombStateData_j[curDWorkIdx] = tmpIntStage;
        MultiWordSub(&tmpIntStage.chunks[0U], &tmpIntStageCurr.chunks[0U],
                     &tmp.chunks[0U], 2);
        MultiWordSignedWrap(&tmp.chunks[0U], 2, 21U, &tmpIntStage.chunks[0U]);

        /* Comb Filter Stage 7
         */
        curDWorkIdx = m1_DW.filter_CircBufCounter_p + 7;
        tmpIntStageCurr = m1_DW.filter_CombStateData_j[curDWorkIdx];
        m1_DW.filter_CombStateData_j[curDWorkIdx] = tmpIntStage;
        m1_DW.filter_CircBufCounter_p++;
        if (m1_DW.filter_CircBufCounter_p >= 1) {
          m1_DW.filter_CircBufCounter_p--;
        }

        MultiWordSub(&tmpIntStage.chunks[0U], &tmpIntStageCurr.chunks[0U],
                     &tmp.chunks[0U], 2);
        MultiWordSignedWrap(&tmp.chunks[0U], 2, 21U, &rtb_filter_h.chunks[0U]);
      }
    }

    /* End of S-Function (sdspcicfilter): '<S6>/filter' */

    /* DataTypeConversion: '<Root>/Data Type Conversion' */
    m1_B.DataTypeConversion = sMultiWord2Double(&rtb_filter.chunks[0U], 2, 0) *
      3.0517578125E-5;

    /* DataTypeConversion: '<Root>/Data Type Conversion1' */
    m1_B.DataTypeConversion1 = sMultiWord2Double(&rtb_filter_h.chunks[0U], 2, 0)
      * 3.0517578125E-5;

    /* DiscreteFir: '<S3>/Digital Filter' */
    /* Consume delay line and beginning of input samples */
    rtb_Add = 0.0;
    while (j < 1) {
      rtb_Add += m1_B.DataTypeConversion * m1_P.DigitalFilter_Coefficients[0];
      j = 1;
    }

    for (j = 0; j < 121; j++) {
      rtb_Add += m1_P.DigitalFilter_Coefficients[1 + j] *
        m1_DW.DigitalFilter_states[j];
    }

    /* Update delay line for next frame */
    for (i = 119; i >= 0; i--) {
      m1_DW.DigitalFilter_states[1 + i] = m1_DW.DigitalFilter_states[i];
    }

    m1_DW.DigitalFilter_states[0] = m1_B.DataTypeConversion;

    /* End of DiscreteFir: '<S3>/Digital Filter' */

    /* DiscreteFir: '<S4>/Digital Filter' */
    /* Consume delay line and beginning of input samples */
    rtb_SineWave1 = 0.0;
    j = 0;
    while (j < 1) {
      rtb_SineWave1 += m1_B.DataTypeConversion1 *
        m1_P.DigitalFilter_Coefficients_f[0];
      j = 1;
    }

    for (j = 0; j < 121; j++) {
      rtb_SineWave1 += m1_P.DigitalFilter_Coefficients_f[1 + j] *
        m1_DW.DigitalFilter_states_g[j];
    }

    /* Update delay line for next frame */
    for (i = 119; i >= 0; i--) {
      m1_DW.DigitalFilter_states_g[1 + i] = m1_DW.DigitalFilter_states_g[i];
    }

    m1_DW.DigitalFilter_states_g[0] = m1_B.DataTypeConversion1;

    /* End of DiscreteFir: '<S4>/Digital Filter' */

    /* RealImagToComplex: '<Root>/Real-Imag to Complex1' */
    m1_B.RealImagtoComplex1.re = rtb_Add;
    m1_B.RealImagtoComplex1.im = rtb_SineWave1;

    /* DownSample: '<Root>/Downsample' */
    if (m1_M->Timing.TaskCounters.TID[2] == 0) {
      m1_B.Downsample = m1_B.RealImagtoComplex1;
    }

    /* End of DownSample: '<Root>/Downsample' */
  }

  /* End of Buffer: '<Root>/Buffer1' */

  /* Matfile logging */
  rt_UpdateTXYLogVars(m1_M->rtwLogInfo, (&m1_M->Timing.taskTime0));

  /* signal main to stop simulation */
  {                                    /* Sample time: [0.0041666666666666666s, 0.0s] */
    if ((rtmGetTFinal(m1_M)!=-1) &&
        !((rtmGetTFinal(m1_M)-m1_M->Timing.taskTime0) > m1_M->Timing.taskTime0 *
          (DBL_EPSILON))) {
      rtmSetErrorStatus(m1_M, "Simulation finished");
    }
  }

  /* Update absolute time for base rate */
  /* The "clockTick0" counts the number of times the code of this task has
   * been executed. The absolute time is the multiplication of "clockTick0"
   * and "Timing.stepSize0". Size of "clockTick0" ensures timer will not
   * overflow during the application lifespan selected.
   * Timer of this task consists of two 32 bit unsigned integers.
   * The two integers represent the low bits Timing.clockTick0 and the high bits
   * Timing.clockTickH0. When the low bit overflows to 0, the high bits increment.
   */
  if (!(++m1_M->Timing.clockTick0)) {
    ++m1_M->Timing.clockTickH0;
  }

  m1_M->Timing.taskTime0 = m1_M->Timing.clockTick0 * m1_M->Timing.stepSize0 +
    m1_M->Timing.clockTickH0 * m1_M->Timing.stepSize0 * 4294967296.0;
  rate_scheduler();
}

/* Model initialize function */
void m1_initialize(void)
{
  /* Registration code */

  /* initialize non-finites */
  rt_InitInfAndNaN(sizeof(real_T));

  /* initialize real-time model */
  (void) memset((void *)m1_M, 0,
                sizeof(RT_MODEL_m1_T));
  rtmSetTFinal(m1_M, -1);
  m1_M->Timing.stepSize0 = 0.0041666666666666666;

  /* Setup for data logging */
  {
    static RTWLogInfo rt_DataLoggingInfo;
    rt_DataLoggingInfo.loggingInterval = NULL;
    m1_M->rtwLogInfo = &rt_DataLoggingInfo;
  }

  /* Setup for data logging */
  {
    rtliSetLogXSignalInfo(m1_M->rtwLogInfo, (NULL));
    rtliSetLogXSignalPtrs(m1_M->rtwLogInfo, (NULL));
    rtliSetLogT(m1_M->rtwLogInfo, "");
    rtliSetLogX(m1_M->rtwLogInfo, "");
    rtliSetLogXFinal(m1_M->rtwLogInfo, "");
    rtliSetLogVarNameModifier(m1_M->rtwLogInfo, "rt_");
    rtliSetLogFormat(m1_M->rtwLogInfo, 4);
    rtliSetLogMaxRows(m1_M->rtwLogInfo, 1000);
    rtliSetLogDecimation(m1_M->rtwLogInfo, 1);
    rtliSetLogY(m1_M->rtwLogInfo, "");
    rtliSetLogYSignalInfo(m1_M->rtwLogInfo, (NULL));
    rtliSetLogYSignalPtrs(m1_M->rtwLogInfo, (NULL));
  }

  /* block I/O */
  (void) memset(((void *) &m1_B), 0,
                sizeof(B_m1_T));

  /* states (dwork) */
  (void) memset((void *)&m1_DW, 0,
                sizeof(DW_m1_T));

  /* Matfile logging */
  rt_StartDataLoggingWithStartTime(m1_M->rtwLogInfo, 0.0, rtmGetTFinal(m1_M),
    m1_M->Timing.stepSize0, (&rtmGetErrorStatus(m1_M)));

  {
    real_T arg;

    /* Start for S-Function (sdsprandsrc2): '<Root>/Random Source' */
    RandSrcCreateSeeds_64((uint32_T)(100000 * rand()),
                          &m1_DW.RandomSource_SEED_DWORK, 1);
    RandSrcInitState_U_64(&m1_DW.RandomSource_SEED_DWORK,
                          m1_DW.RandomSource_STATE_DWORK, 1);

    /* Start for S-Function (sdspsine2): '<Root>/Sine Wave' */
    /* Trigonometric mode: compute accumulated
       normalized trig fcn argument for each channel */
    /* Keep normalized value in range [0 2*pi) */
    arg = fmod(m1_P.SineWave_Phase, 6.2831853071795862);
    if (arg < 0.0) {
      arg += 6.2831853071795862;
    }

    m1_DW.SineWave_AccFreqNorm = arg;

    /* End of Start for S-Function (sdspsine2): '<Root>/Sine Wave' */

    /* Start for S-Function (sdspsine2): '<Root>/Sine Wave1' */
    /* Trigonometric mode: compute accumulated
       normalized trig fcn argument for each channel */
    /* Keep normalized value in range [0 2*pi) */
    arg = fmod(m1_P.SineWave1_Phase, 6.2831853071795862);
    if (arg < 0.0) {
      arg += 6.2831853071795862;
    }

    m1_DW.SineWave1_AccFreqNorm = arg;

    /* End of Start for S-Function (sdspsine2): '<Root>/Sine Wave1' */

    /* Start for S-Function (sdspsine2): '<Root>/Sine Wave2' */
    /* Trigonometric mode: compute accumulated
       normalized trig fcn argument for each channel */
    /* Keep normalized value in range [0 2*pi) */
    arg = fmod(m1_P.SineWave2_Phase, 6.2831853071795862);
    if (arg < 0.0) {
      arg += 6.2831853071795862;
    }

    m1_DW.SineWave2_AccFreqNorm = arg;

    /* End of Start for S-Function (sdspsine2): '<Root>/Sine Wave2' */
  }

  {
    real_T arg;
    int32_T i;
    static int64m_T tmp = { { 0U, 0U } /* chunks */
    };

    /* InitializeConditions for S-Function (sdspsine2): '<Root>/Sine Wave' */
    /* This code only executes when block is re-enabled in an
       enabled subsystem when the enabled subsystem states on
       re-enabling are set to 'Reset' */
    /* Reset to time zero on re-enable */
    /* Trigonometric mode: compute accumulated
       normalized trig fcn argument for each channel */
    /* Keep normalized value in range [0 2*pi) */
    arg = fmod(m1_P.SineWave_Phase, 6.2831853071795862);
    if (arg < 0.0) {
      arg += 6.2831853071795862;
    }

    m1_DW.SineWave_AccFreqNorm = arg;

    /* End of InitializeConditions for S-Function (sdspsine2): '<Root>/Sine Wave' */

    /* InitializeConditions for S-Function (sdspsine2): '<Root>/Sine Wave1' */
    /* This code only executes when block is re-enabled in an
       enabled subsystem when the enabled subsystem states on
       re-enabling are set to 'Reset' */
    /* Reset to time zero on re-enable */
    /* Trigonometric mode: compute accumulated
       normalized trig fcn argument for each channel */
    /* Keep normalized value in range [0 2*pi) */
    arg = fmod(m1_P.SineWave1_Phase, 6.2831853071795862);
    if (arg < 0.0) {
      arg += 6.2831853071795862;
    }

    m1_DW.SineWave1_AccFreqNorm = arg;

    /* End of InitializeConditions for S-Function (sdspsine2): '<Root>/Sine Wave1' */

    /* InitializeConditions for S-Function (sdspsine2): '<Root>/Sine Wave2' */
    /* This code only executes when block is re-enabled in an
       enabled subsystem when the enabled subsystem states on
       re-enabling are set to 'Reset' */
    /* Reset to time zero on re-enable */
    /* Trigonometric mode: compute accumulated
       normalized trig fcn argument for each channel */
    /* Keep normalized value in range [0 2*pi) */
    arg = fmod(m1_P.SineWave2_Phase, 6.2831853071795862);
    if (arg < 0.0) {
      arg += 6.2831853071795862;
    }

    m1_DW.SineWave2_AccFreqNorm = arg;

    /* End of InitializeConditions for S-Function (sdspsine2): '<Root>/Sine Wave2' */

    /* InitializeConditions for Buffer: '<Root>/Buffer' */
    m1_DW.Buffer_inBufPtrIdx = 10;
    m1_DW.Buffer_bufferLength = 10;
    m1_DW.Buffer_outBufPtrIdx = 0;
    for (i = 0; i < 20; i++) {
      m1_DW.Buffer_CircBuf[i] = m1_P.Buffer_ic;

      /* InitializeConditions for Buffer: '<Root>/Buffer1' */
      m1_DW.Buffer1_CircBuf[i] = m1_P.Buffer1_ic;
    }

    /* End of InitializeConditions for Buffer: '<Root>/Buffer' */

    /* InitializeConditions for Buffer: '<Root>/Buffer1' */
    m1_DW.Buffer1_inBufPtrIdx = 10;
    m1_DW.Buffer1_bufferLength = 10;
    m1_DW.Buffer1_outBufPtrIdx = 0;

    /* InitializeConditions for S-Function (sdspcicfilter): '<S5>/filter' */
    m1_DW.filter_CircBufCounter = 0;

    /* InitializeConditions for S-Function (sdspcicfilter): '<S6>/filter' */
    m1_DW.filter_CircBufCounter_p = 0;
    for (i = 0; i < 8; i++) {
      /* InitializeConditions for S-Function (sdspcicfilter): '<S5>/filter' */
      m1_DW.filter_CICStates[i] = tmp;
      m1_DW.filter_CombStateData[i] = tmp;

      /* InitializeConditions for S-Function (sdspcicfilter): '<S6>/filter' */
      m1_DW.filter_CICStates_l[i] = tmp;
      m1_DW.filter_CombStateData_j[i] = tmp;
    }

    for (i = 0; i < 121; i++) {
      /* InitializeConditions for DiscreteFir: '<S3>/Digital Filter' */
      m1_DW.DigitalFilter_states[i] = m1_P.DigitalFilter_InitialStates;

      /* InitializeConditions for DiscreteFir: '<S4>/Digital Filter' */
      m1_DW.DigitalFilter_states_g[i] = m1_P.DigitalFilter_InitialStates_k;
    }
  }
}

/* Model terminate function */
void m1_terminate(void)
{
  /* (no terminate code required) */
}
