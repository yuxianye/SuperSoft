SuperSoft�����������

1.������������������浥�����͵����ϵͳ���ܹ����ٴ�������ݹ������͵�ϵͳ��ר�����
2.���ݿ����SQL Server LocalDB����������С������������Ŀ��
3.UI��������WPF���������MVVMģʽ��������������MahApps.Metro��Fluent��GalaSoft.MvvmLightʵ�ֽ������߼�
4.��������������Ŀ�ṹ���£�
	SuperSoft.App		��ִ��������Ŀ
	SuperSoft.BLL		ҵ���߼�
	SuperSoft.DAL		���ݷ��ʲ㣬����΢��SQLHelper V2.0�������ݿ⣬Ҳ�ɸ����������ݿ���������������Ҫ�󲻸߿��Բ���Entity Framework��
	SuperSoft.Model		ģ��ʵ�壬ö������
	SuperSoft.Utility	����
	SuperSoft.View		ҳ����ͼ�߼�
			UserControl	�ؼ�

			View		��ͼ
						ViewInfo			��ͼ��Ϣ
						MainView			����ͼ
						PatientListView		�����б���ͼ
						xxxView				xx��ͼ
			ViewModel	ҳ����ص�ViewModel
						MainViewModel							����ͼ�Ĵ����߼�
						MyViewModelBase							�̳��� GalaSoft.MvvmLight ViewModelBase �� IDisposable,���ڴ���Parameter����
						Token									���ƶ���
						ViewModelLocator						ViewModel��λ����
						PatientListViewModel					�����б�ҵ�����߼�
						xxxViewModel							xxxҵ�����߼�
	SuperSoft.Resource											��Դ�ļ��У���APP�и��������ļ����ز�ͬ���Ե���Դ����Դ�����������֡��ؼ����֡��ؼ���ʽ�ȣ�
			SuperSoft.Resource.Default							ͨ�õ���Դ���������ԣ����ڴ洢ͼ�Ρ�ͼƬ�ȣ�ʹ��StaticResource��
										Images					ͼ��
											Images.cdr			ͼ���CorelDRAWԴ�ļ�
											Add_32x32.png		ͼ��32x32����
										ColorBrush.xaml			��ɫ�����Դ
										DefaultResources.xaml	��Դ���������
										SharpBrush.xaml			��״��Դ
										SingleValue.xaml		������ֵ��Դ������ؼ���ȵ�
			SuperSoft.Resource.en-US			Ӣ����ص���Դ��ʹ��DynamicResource�󶨣��л�����ʱ��̬�ı�
			SuperSoft.Resource.zh-CN			������ص���Դ��ʹ��DynamicResource�󶨣��л�����ʱ��̬�ı�
			SuperSoft.Resource.xx-xx			xx����ص���Դ(��չ������)
5.������Ŀ���ɵ���Ŀ¼ bin\Debug ���� bin\Release����
6.��Ŀ¼�µġ�clean-with-scc.bat���ļ�����ɾ����Ŀ¼�е�Դ������ļ�����Ŀ���ɵ���ʱ�ļ����ļ���

����˵����
	1).��SuperSoft.App����Ŀ�������øĶ���������Ŀ����ҵ��������Ӧ��ҵ���߼�
	2).ҳ����ת������Ϣ֪ͨ��ʽ��ҳ��������÷��䡣
	3).���˵�״̬��������Ϣ֪ͨ��ʽ��
	4).���ݷ��ʵ������ԣ����ݷ��ʲ����������صĹ��ܣ�ʹ��DbTransaction��
	5).���������ʹ�õĵ��������������MahApps.Metro��Fluent��SQLHelper V2.0��GalaSoft.MvvmLight������nuget��ȡ��
		��ϸ��Ϣ�ɲο� https://github.com/ https://www.nuget.org/ 







            
