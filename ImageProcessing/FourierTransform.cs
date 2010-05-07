using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fftwlib;
using System.Runtime.InteropServices;

namespace ImageProcessing
{
  public static class FourierTransform
  {
    public static CImage<Complex> ForwardFFT2D(CImage<Complex> img)
    {
      IntPtr pin, pout;
      IntPtr fplan1; //fplan2, fplan3;
      int h = img.GetH;
      int w = img.GetW;

      pin = fftw.malloc(h * w * 16);
      pout = fftw.malloc(h * w * 16);

      unsafe
      {
        double* pfin = (double*)pin;
        double* pfout = (double*)pout;

        for (int i = 0; i < h; i++)
        {
          for (int j = 0; j < w; j++)
          {
            pfin[(i * h + j) * 2] = img[i, j].Re;
            pfin[(i * h + j) * 2 + 1] = img[i, j].Im;
          }

        }

      }
      fplan1 = fftw.dft_2d(h, w, pin, pout, fftw_direction.Forward, fftw_flags.Estimate);
      fftw.execute(fplan1);

      CImage<Complex> cImg = new CImage<Complex>(h, w);
      unsafe
      {
        double* buf = (double*)pout;
        for (int i = 0; i < h; i++)
        {
          for (int j = 0; j < w; j++)
          {
            cImg[i, j].Re = buf[(i * h + j) * 2];
            cImg[i, j].Im = buf[(i * h + j) * 2 + 1];
          }
        }
      }

      fftw.free(pin);
      fftw.free(pout);
      fftw.destroy_plan(fplan1);
      return cImg;
    }

    public static CImage<Complex> BackwardFFT2D(CImage<Complex> img)
    {
      IntPtr pin, pout;
      IntPtr fplan1; //fplan2, fplan3;
      int h = img.GetH;
      int w = img.GetW;

      pin = fftw.malloc(h * w * 16);
      pout = fftw.malloc(h * w * 16);

      unsafe
      {
        double* pfin = (double*)pin;
        double* pfout = (double*)pout;

        for (int i = 0; i < h; i++)
        {
          for (int j = 0; j < w; j++)
          {
            pfin[(i * h + j) * 2] = img[i, j].Re;
            pfin[(i * h + j) * 2 + 1] = img[i, j].Im;
          }

        }

      }
      fplan1 = fftw.dft_2d(h, w, pin, pout, fftw_direction.Backward, fftw_flags.Estimate);
      fftw.execute(fplan1);

      CImage<Complex> cImg = new CImage<Complex>(h, w);
      unsafe
      {
        double* buf = (double*)pout;
        for (int i = 0; i < h; i++)
        {
          for (int j = 0; j < w; j++)
          {
            cImg[i, j].Re = buf[(i * h + j) * 2];
            cImg[i, j].Im = buf[(i * h + j) * 2 + 1];
          }
        }
      }

      fftw.free(pin);
      fftw.free(pout);
      fftw.destroy_plan(fplan1);
      return cImg;
    }

  }
}
