
void timepoints(){


Data=csvread('S01_LB01_P.csv',1,1);
Spine=Data(:,2);
Time=Data(:,1)/1000;
startindex = find(diff(Spine) < -40);


X=diff((-Spine));
[b, a]=butter(4,0.1);
F=filter(b,a,Spine);
Y=diff(F);
[pks,locs]=findpeaks(-F,'MinPeakProminence',1,'MinPeakHeight',10,'MinPeakWidth',10);
[pks2,locs2]=findpeaks(F,'MinPeakProminence',1,'MinPeakHeight',5,'MinPeakWidth',5);

m=size(pks2);

a=1;
b=1;
for i=1:locs(1)
    if abs(X(i))<10 
        if abs(X(i+1))<10
            if abs(X(i+2))<10
                if abs(X(i+3))<10
                    if abs(X(i+4))<10
                        if abs(X(i+5))<10
                            if Spine(i+4)>-50
                                start(a)=i+6;
                                a=a+1;
                            end
                        end
                    end
                end
            end
        end
        
    end
   
end
figure(1)
plot(Spine)

StartPoint=start(a-1)
EndPoint=locs(1)


}
