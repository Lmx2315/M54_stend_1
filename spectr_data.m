clear all
close all

%pkg load signal;%����������������� ��� OCTAVE,���������������� ��� MATLAB

N=4096;
fs=6250000;
B=16; %h����������� ������� ������
k=2^(B-1);

% load a txt file
fid1 = fopen('data_2.dat','r');
y=load ("data_2.dat");
fclose(fid1);
y=y/k; %��������� ������� ������ � �������

DatRe=y(:,1); %�������� ������ ������� ������� � ������ I
DatIm=y(:,2); %�������� ������ ������� ������� � ������ Q

DAT_COMPLEX=DatRe + j*DatIm;
y=DAT_COMPLEX';%������������� ������� � ������

%w=blackman (N); %����������� ������������ ����
 w=hann     (N); %����������� ������������ ����
%w=bartlett (N); %����������� ������������ ����
 
w=w';%����������������
y=y.*w;
Y=fft(y);
Q=fftshift(Y);%������� ����������� ������ ����� "0" ��� � ������
A=abs(Q);%������� ������ ������������ ����� ������� �������� �������
A=A';
F  =20*log10(A);
F  =F.-52.4; %������� ����� 
Fl=(-N/2:N/2-1)*(fs/N);

figure(1)
plot(Fl,F);
ylim([-70 15])

% load a txt file
fid1 = fopen('data_1.dat','r');
y=load ("data_1.dat");
fclose(fid1);
y=y/k; %��������� ������� ������ � �������

DatRe=y(:,1); %�������� ������ ������� ������� � ������ I
DatIm=y(:,2); %�������� ������ ������� ������� � ������ Q

DAT_COMPLEX=DatRe + j*DatIm;
y=DAT_COMPLEX';%������������� ������� � ������

%w=blackman (N); %����������� ������������ ����
 w=hann     (N); %����������� ������������ ����
%w=bartlett (N); %����������� ������������ ����
 
w=w';%����������������
y=y.*w;
Y=fft(y);
Q=fftshift(Y);%������� ����������� ������ ����� "0" ��� � ������
A=abs(Q);%������� ������ ������������ ����� ������� �������� �������
A=A';
F  =20*log10(A);
F  =F.-52.4; %������� ����� 
Fl=(-N/2:N/2-1)*(fs/N);

figure(2)
plot(Fl,F);
ylim([-70 15])