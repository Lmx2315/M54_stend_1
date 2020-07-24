clear all
close all

pkg load signal;

N=4096;
fs=6250000;
B=16; %h����������� ������� ������
k=2^(B-1);

% load a txt file
fid1 = fopen('data_1.dat','r');
y=load ("data_1.dat");
fclose(fid1);
y=y/k; %��������� ������� ������ � �������

DatRe=y(:,1); %�������� ������ ������� ������� � ������ I
DatIm=y(:,2); %�������� ������ ������� ������� � ������ Q

DAT_COMPLEX=DatRe + j*DatIm;
y=DAT_COMPLEX';%������������� ������� � ������

 w=blackman (N); %����������� ������������ ����
%w=hann     (N); %����������� ������������ ����
w=w';%����������������
y=y.*w;
Y=fft(y);
Y=fftshift(Y);%������� ����������� ������ ����� "0" ��� � ������
A=abs(Y);%������� ������ ������������ ����� ������� �������� �������
A=A';
F  =10*log(A);
Fl=(-N/2:N/2-1)*(fs/N);

figure(1)
plot(Fl,F);
