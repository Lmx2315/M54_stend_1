clear all
close all

pkg load signal;

N=4096;
fs=6250000;
B=16; %hразрядность входных данных
k=2^(B-1);

% load a txt file
fid1 = fopen('data_1.dat','r');
y=load ("data_1.dat");
fclose(fid1);
y=y/k; %нормируем входные данные к еденице

DatRe=y(:,1); %копируем первый столбец матрицы в вектор I
DatIm=y(:,2); %копируем второй столбец матрицы в вектор Q

DAT_COMPLEX=DatRe + j*DatIm;
y=DAT_COMPLEX';%транспарируем столбэц в вектор

 w=blackman (N); %расчитываем коэффициенты окна
%w=hann     (N); %расчитываем коэффициенты окна
w=w';%транспорирование
y=y.*w;
Y=fft(y);
Y=fftshift(Y);%смещаем комплексный спектр чтобы "0" был в центре
A=abs(Y);%считает модуль комплексного числа каждого элемента вектора
A=A';
F  =10*log(A);
Fl=(-N/2:N/2-1)*(fs/N);

figure(1)
plot(Fl,F);
