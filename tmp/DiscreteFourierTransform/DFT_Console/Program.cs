using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DiscreteFourierTransform;
using System.Numerics;

namespace DFT_Console
{
    class Program
    {
        static void Main(string[] args)
        {

            DiscreteFourierTransform.DiscreteFourierTransform dft = new DiscreteFourierTransform.DiscreteFourierTransform();

            Double[] signal = dft.testsignal();
            int[] n = dft.testsignalindexes();


            // --- Discrete Fourier Transform by definition

            Complex[] X = dft.DFT(signal); // discrete fourier transform by definition

            Double[] A = dft.Amplitude(X); // amplitude spectrum from X

            Complex[] Xa = dft.DFT(signal, n); // discrete fourier transform by definition with samples numbers

            Double[] signal2 = dft.Shift(signal); // Signal shift method

            Complex[] Xb = dft.DFT(signal2); // discrete fourier transform by definition of shifted signal

            // Compare Xa and Xb and see if we were correct!


            // --- Inverse Discrete Fourier Transform by definition

            Double[] x = dft.iDFT(X); // inverse discrete fourier transform by definition

            Double[] xa = dft.iDFT(Xa, n); // inverse discrete fourier transform by definition (with samples numbers)

            Double[] xb = dft.iDFT(Xb); // inverse discrete fourier transform by definition of shifted signal

            // Compare x, xa and xb. Any other operation needs to be performed at xb to restore original?



            // --- Discrete Fourier Transform with reduced number of calculation

            Complex[] X2 = dft.DFT2(signal); // discrete fourier transform with reduced number of calculation

            Complex[] X2a = dft.DFT2(signal, n); // discrete fourier transform with reduced number of calculation with samples numbers

            Complex[] X2b = dft.DFT2(dft.Shift(signal), n); // discrete fourier transform with reduced number of calculation (shifted signal)



            // --- Fast Fourier Transform


            // Signal pack with zeros

            Double[] signalpack = dft.SignalPackRight(signal); // Complement at right to N = nearest power of 2

            Double[] signalpack2 = dft.SignalPackLeft(signal); // Complement at left to N = nearest power of 2

            Double[] signalpack3 = dft.SignalPackBoth(signal); // Complement at both sides to N = nearest power of 2


            // Samples sorting

            Double[] signal3 = dft.FFTDataSort(signalpack); // using bit reverse

            Double[] signal4 = dft.FFTDataSort2(signalpack); // using shift method

            // Compare signal3 and signal4 - should be equal!


            Complex[] X3 = dft.FFT(signal3); // fast fourier transform

            Complex[] X3a = dft.FFT(dft.Shift(signal3)); // shifted spectrum - fast fourier transform


            // --- Inverse Fast Fourier Transform

            Complex[] X3s = dft.FFTDataSort(X3); // spectrum sort using bit reverse

            Complex[] X3as = dft.FFTDataSort2(X3); // spectrum sort using shift method


            Double[] x3 = dft.iFFT(X3s); // inverse fast fourier transform

            // Compare x3 with signal!


        }
    }
}
