SuperSoft解决方案介绍

1.本解决方案适用于桌面单机类型的软件系统。能够快速搭建各种数据管理类型的系统和专用软件
2.数据库采用SQL Server LocalDB，能满足中小型数据量的项目。
3.UI界面层采用WPF技术，结合MVVM模式，借助第三方库MahApps.Metro、Fluent、GalaSoft.MvvmLight实现界面层的逻辑
4.本解决方案类库项目结构如下：
	SuperSoft.App		可执行启动项目
	SuperSoft.BLL		业务逻辑
	SuperSoft.DAL		数据访问层，采用微软SQLHelper V2.0访问数据库，也可更换其他数据库组件，如果对性能要求不高可以采用Entity Framework。
	SuperSoft.Model		模型实体，枚举类型
	SuperSoft.Utility	工具
	SuperSoft.View		页面视图逻辑
			UserControl	控件

			View		视图
						ViewInfo			视图信息
						MainView			主视图
						PatientListView		患者列表视图
						xxxView				xx视图
			ViewModel	页面相关的ViewModel
						MainViewModel							主视图的处理逻辑
						MyViewModelBase							继承自 GalaSoft.MvvmLight ViewModelBase 和 IDisposable,用于传递Parameter参数
						Token									令牌定义
						ViewModelLocator						ViewModel定位程序
						PatientListViewModel					患者列表业务处理逻辑
						xxxViewModel							xxx业务处理逻辑
	SuperSoft.Resource											资源文件夹（在APP中根据配置文件加载不同语言的资源，资源包括界面文字、控件布局、控件样式等）
			SuperSoft.Resource.Default							通用的资源，不分语言，用于存储图形、图片等，使用StaticResource绑定
										Images					图像
											Images.cdr			图像的CorelDRAW源文件
											Add_32x32.png		图像32x32像素
										ColorBrush.xaml			颜色相关资源
										DefaultResources.xaml	资源总引用入口
										SharpBrush.xaml			形状资源
										SingleValue.xaml		单个的值资源，例如控件宽度等
			SuperSoft.Resource.en-US			英文相关的资源，使用DynamicResource绑定，切换语言时动态改变
			SuperSoft.Resource.zh-CN			中文相关的资源，使用DynamicResource绑定，切换语言时动态改变
			SuperSoft.Resource.xx-xx			xx文相关的资源(扩展多语言)
5.所有项目生成到根目录 bin\Debug 或者 bin\Release下面
6.根目录下的【clean-with-scc.bat】文件用于删除子目录中的源码管理文件和项目生成的临时文件和文件夹

其他说明：
	1).【SuperSoft.App】项目基本不用改动。其他项目根据业务增加相应的业务逻辑
	2).页面跳转采用消息通知方式，页面加载利用反射。
	3).主菜单状态等利用消息通知方式。
	4).数据访问的事务性：数据访问层具有事物相关的功能，使用DbTransaction。
	5).本解决方案使用的第三方组件包括：MahApps.Metro、Fluent、SQLHelper V2.0、GalaSoft.MvvmLight，采用nuget获取。
		详细信息可参考 https://github.com/ https://www.nuget.org/ 







            
