   M S B A M L   `   `   `  ���� B  >WpfApp2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp2WpfApp2      WpfApp2.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     'localclr-namespace:WpfApp2       � Title$  
MainWindow��$	��730��$
��1100��.����  �� Padding$ 15q�$��	0,10,0,10q�._���  �� Orientation$ Vertical=�$��0,0,0,0q�.+�� $	��100��$
��Left=�$
��1081��$��0,0,-3.667,0q� � ColumnDefinitions �� $
��541* ��� �� $
��539* ��� .+�� $��0,0,0,0q� � RowDefinitions �� $��* ��� $��2* � �� $
��931* ��� $
��132* �.+��� $��0��$��0��$��1,-99,-1.667,99.333q�$	��673��$��Bottom=�$��2��.@��� -    ��Name$ 	lvPatient��$��Patient Management��$��1��$��0,-2,-0.333,0q�$
��Left=�$	��123��$��20��$	��Top=�.��� ���	#FFE5E5E5$��-16,-4,17,-70.667q� �� $��1* ��� $��1* � �� $	��60* ��� $	��23* ��� $	��116 ��� $	��272 �.+��� -    ��Name$ 
lvPatients��$
��Left=�$��14,8,0,0q���.  $��0��$
��1035��$��5������  �� View �� .P��� $��
Patient_Id�� �� DisplayMemberBinding �� *
Patient_Id+�� $��Medicare No�� �� *Medicare_No+�� $��
First Name�� �� *	FirstName+�� $��Surname�� �� *
LastName+�� $��Gender�� �� *Gender+�� $	��DOB�� �� *DateOfBirth+	 �� StringFormat$	 
dd/MM/yyyy���� $��Address�� �� *StreetAddress+�� $��Suburb�� �� *Suburb+�� $��State�� �� *State+�� $��	Post Code�� �� *
PostCode+�� $��
Home Phone�� �� *	Homephone+�� $��Mobile Phone�� �� *Mobilephone+�� $��Notes�� �� *Notes+� $��1��$	��348��$	��Top=�$��5��$��3��$��0,0,-53.667,0q� �� $��1* ��� $��2* ��� $��2* ��� $��1* �.+��� -   
 ��Name$	
 Add��$	��ADD��$
��Left=�$��29��$��
35,129,0,0q�$	��Top=�$��70��$��Segoe UI Semibold)�$��10(� J� RenderTransformOrigin$ 0.568,-3.621&�$��3���� -   $
 Search��$��SEARCH��$
��Left=�$��29��$��	35,16,0,0q�$	��Top=�$��70��$��Segoe UI Semibold)�$��10(�$ 1.165,-2.172&�$��3���� -   $
 update��$��UPDATE��$
��Left=�$��29��$��
35,172,0,0q�$	��Top=�$��70��$��Segoe UI Semibold)�$��10(�$��3���� -   $
 delete��$��DELETE��$
��Left=�$��29��$��
35,217,0,0q�$	��Top=�$��70��$��Segoe UI Semibold)�$��10(�$��3���� -   $
 clear��$��CLEAR��$
��Left=�$��29��$��
36,262,0,0q�$	��Top=�$��70��$��Segoe UI Semibold)�$��10(�$��3���� $
��Left=�$��18��$��
485,58,0,0q�$
��Wrap=�$��Patient's  Notes:��$	��Top=�$��89��$��Segoe UI Semibold)�$��10(�$ 0.493,0.556&�$��1��$��2���� -    ��Name$ notes��$��Expanded&�$
��Left=�$	��245��$��
483,86,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��460��$��Segoe UI Semibold)�$��10(�$ 0.488,0.535&�$��1��$��3��$��2�� � DataContext �� *SelectedItem+ �� ElementName$ 
lvPatients������ *Notes+ �� Mode$ OneWay=��� -	   $ fname��$
��Left=�$��24��$��
284,63,0,0q�$
 Wrap=�$	��Top=�$	��178��$��Segoe UI Semibold)�$��10(�$��1�� �� *SelectedItem+$ 
lvPatients������ *	FirstName+$ OneWay=��� -
   $ lname��$
��Left=�$��24��$��
284,91,0,0q�$
 Wrap=�$	��Top=�$	��178��$��Segoe UI Semibold)�$��10(�$ 	0.491,0.5&�$��1��$��2�� �� *SelectedItem+$ 
lvPatients������ *
LastName+$ OneWay=��� -   $	 Mno��$
��Left=�$��24��$��
284,36,0,0q�$
 Wrap=�$	��Top=�$	��178��$��Segoe UI Semibold)�$��10(�$��1�� �� *SelectedItem+$ 
lvPatients������ *Medicare_No+$ OneWay=��� -   $ Homeno��$
��Left=�$��24��$��
284,80,0,0q�$
 Wrap=�$	��Top=�$	��178��$��Segoe UI Semibold)�$��10(�$ 0.493,0.533&�$��3�� �� *SelectedItem+$ 
lvPatients������ *	Homephone+$ OneWay=��� -   $ Mobileno��$
��Left=�$��24��$��284,108,0,0q�$
 Wrap=�$	��Top=�$	��178��$��Segoe UI Semibold)�$��10(�$ 	0.5,0.524&�$��3�� �� *SelectedItem+$ 
lvPatients������ *Mobilephone+$ OneWay=��� -    ��Name$ patient_gender��$
��Left=�$��24��$��
284,29,0,0q�$	��Top=�$	��178��$��10(�$��2�� �� *SelectedItem+$ 
lvPatients�� �� Text �� *Gender+$ OneWay=�.@�	�  �� 	ClickMode$ Press=�$
��Male��	� $ Press=�$��Female��	� $ Press=�$��Other��(  "System.Windows.Controls.DatePicker  -     Name$ patient_date��$
��Left=�$��24��$��
284,60,0,0q�$	��Top=�$	��178��$��10(�$��2�� �� *SelectedItem+$ 
lvPatients��   Text �� *DateOfBirth+$ OneWay=��� -   $ patient_address��$
��Left=�$��24��$��
284,88,0,0q�$
 Wrap=�$	��Top=�$	��178��$��Segoe UI Semibold)�$��10(�$ 	0.491,0.5&�$��2�� �� *SelectedItem+$ 
lvPatients������ *StreetAddress+$ OneWay=��� -   $ patient_suburb��$
��Left=�$��24��$��284,115,0,0q�$
 Wrap=�$	��Top=�$	��178��$��Segoe UI Semibold)�$��10(�$ 	0.491,0.5&�$��2��$��2�� �� *SelectedItem+$ 
lvPatients������ *Suburb+$ OneWay=��� -   $ patient_postcode��$
��Left=�$��24��$��
284,53,0,0q�$
 Wrap=�$	��Top=�$	��178��$��Segoe UI Semibold)�$��10(�$ 	0.491,0.5&�$��3�� �� *SelectedItem+$ 
lvPatients������ *
PostCode+$ OneWay=��� $
��Left=�$��15��$��
215,70,0,0q�$
��Wrap=�$��First Name:��$	��Top=�$��55��$��Segoe UI Semibold)�$��10(�$��1���� $
��Left=�$��15��$��	217,4,0,0q�$
��Wrap=�$��
Last Name:��$	��Top=�$��54��$��Segoe UI Semibold)�$��10(�$��2���� $
��Left=�$��17��$��
185,43,0,0q�$
��Wrap=�$��Medicare Number:��$	��Top=�$��92��$��Segoe UI Semibold)�$��10(�$ 1.926,-3.286&�$��1���� $
��Left=�$��15��$��
230,33,0,0q�$
��Wrap=�$��Gender:��$	��Top=�$��41��$��Segoe UI Semibold)�$��10(�$��2���� $
��Left=�$��16��$��
202,93,0,0q�$
��Wrap=�$��Street Address:��$	��Top=�$��74��$��Segoe UI Semibold)�$��10(�$��2���� $
��Left=�$��15��$��	232,4,0,0q�$
��Wrap=�$��Suburb:��$	��Top=�$��37��$��Segoe UI Semibold)�$��10(�$��3���� $
��Left=�$��14��$��
224,57,0,0q�$
��Wrap=�$��	Postcode:��$	��Top=�$��46��$��Segoe UI Semibold)�$��10(�$��3���� $
��Left=�$��19��$��
185,86,0,0q�$
��Wrap=�$	��Top=�$��86��$��Segoe UI Semibold)�$��10(�$��3���� $�Telephone(Home���� $�):���� $
��Left=�$��20��$��181,112,0,0q�$
��Wrap=�$��Telephone(Mobile):��$	��Top=�$��89��$��Segoe UI Semibold)�$��10(�$��3���� $
��Left=�$��15��$��
206,62,0,0q�$
��Wrap=�$��Date of Birth:��$	��Top=�$��66��$��Segoe UI Semibold)�$��10(�$��2���� $
��Left=�$��16��$��
219,16,0,0q�$
��Wrap=�$��Patient ID:��$	��Top=�$��52��$��Segoe UI Semibold)�$��10(�$ 1.926,-3.286&�$��1���� -   $ txtPatient_id�� �� 
IsReadOnly . $
��Left=�$��24��$��	284,9,0,0q�$
 Wrap=�$	��Top=�$	��178��$��Segoe UI Semibold)�$��10(�$��1��{�.   �� *SelectedItem+$ 
lvPatients������ *
Patient_Id+$ OneWay=��� $
��Left=�$��15��$��
241,30,0,0q�$
��Wrap=�$��State:��$	��Top=�$��26��$��Segoe UI Semibold)�$��10(�$��3���� -   $ patient_state��$
��Left=�$��24��$��
284,26,0,0q�$
 Wrap=�$	��Top=�$	��178��$��Segoe UI Semibold)�$��10(�$ 	0.491,0.5&�$��3�� �� *SelectedItem+$ 
lvPatients������ *State+$ OneWay=��� -   $ cboFieldNames��$
��Left=�$��
502,18,0,0q�$	��Top=�$	��189��$��15��$��1��$��23��$��2���� -   $ txtSearchString��$
��Left=�$��23��$��199.333,17,0,0q�$
 Wrap=�
���   �$	��Top=�$	��158��$��16��$��1��$��1��$ -0.527,0.841&��� -   $ lvPractitioner��$��Practitioner Management��$��0.333,-2,0.333,0q�$��20��$	��Top=�����  �� EndPoint$	 0,1&� �� 
StartPoint$	 0,0&�.���  � Color$ 	#FFF0F0F0�� � Offset$ 0Q�� $ 	#FF8E9894��$ 1Q�� $ 	#FFE7E8E7��$ 0.087Q�.��� ���	#FFE5E5E5$��-8,0,8,0q�$	��515�� �� $
��452* ��� $
��619* � �� $��* ��� $��2* �.+�� ���	#FFE5E5E5$��-5,-2,-3,-9.333q�$��2��$��2��$	��526��$	��Top=� �� $	��45* ��� $	��37* ��� $
��439* �.+��� -   $��lvPractitioners��$	��211��$	��Top=�$��0��$��3��
�������$��10,-61,-3,0q����	#FF5EB24C����  �� .P��� $��Practitioner ID�� �� *	Prac_Id+�� $��Medicare Registration No�� �� *
MedRegNo+�� $��
First Name�� �� *	FirstName+�� $��Surname�� �� *
LastName+�� $��Description�� �� *Desc+�� $	��DOB�� �� *DateOfBirth+$	 
dd/MM/yyyy���� $��Gender�� �� *Gender+�� $��Address�� �� *StreetAddress+�� $��Suburb�� �� *Suburb+�� $��State�� �� *State+�� $��	Post Code�� �� *
PostCode+�� $��
Home Phone�� �� *	Homephone+�� $��Mobile Phone�� �� *Mobilephone+�� $��Monday�� �� *Monday+�� $��Tuesday�� �� *	Tuesday+�� $��	Wednesday�� �� *	Wednesday+�� $��Friday�� �� *
Thursday+�� $��Saturday�� �� *Friday+�� $��Sunday�� �� *
Saturday+�� $��Sunday�� �� *Sunday+� $��	0,0,149,0q�$��3�� �� $
��208* ��� $
��415* ��� $
��336* ��� $
��285* �.+��� -   $

 Add1��$	��ADD��$
��Left=�$��29��$��
11,313,0,0q�$	��Top=�$��89��$��Segoe UI Semibold)�$��10(�$ 0.568,-3.621&�$��3���� -   $
 Search1��$��SEARCH��$
��Left=�$��29��$��11.667,261,0,0q�$	��Top=�$��89��$��Segoe UI Semibold)�$��10(�$ 1.165,-2.172&�$��3���� -   $
 Update1��$��UPDATE��$
��Left=�$��29��$��
11,360,0,0q�$	��Top=�$��89��$��Segoe UI Semibold)�$��10(�$��3���� -   $
 Delete1��$��DELETE��$
��Left=�$��29��$��
11,405,0,0q�$	��Top=�$��89��$��Segoe UI Semibold)�$��10(�$��3���� -   $
 Clear1��$��CLEAR��$
��Left=�$��29��$��
11,450,0,0q�$	��Top=�$��89��$��Segoe UI Semibold)�$��10(�$��3���� $
��Left=�$��23��$��
11,245,0,0q�$
��Wrap=�$#��Days of the Week availability��$	��Top=�$	��200��$��Segoe UI Semibold)�$��10(�$��2���� -    ��Name$ 	Mondaychk��$��Monday��$
��Left=�$��
62,275,0,0q�$	��Top=�$��10(�$��Segoe UI Semibold)�$��17��$��90��$��2�� �� *SelectedItem+$ lvPractitioners�� d� 	IsChecked �� *Monday+$ OneWay=��� -   $ 
Tuesdaychk��$��Tuesday��$
��Left=�$��
62,307,0,0q�$	��Top=�$��10(�$��Segoe UI Semibold)�$��17��$��89��$��2�� �� *SelectedItem+$ lvPractitioners�� �� *	Tuesday+$ OneWay=��� -    $ Wednesdaychk��$��	Wednesday��$
��Left=�$��
62,343,0,0q�$	��Top=�$��10(�$��Segoe UI Semibold)�$��15��$	��117��$��2�� �� *SelectedItem+$ lvPractitioners�� �� *	Wednesday+$ OneWay=��� -!   $ ThursdayChk��$��Thursday��$
��Left=�$��
62,375,0,0q�$	��Top=�$��10(�$��Segoe UI Semibold)�$��17��$��98��$��2�� �� *SelectedItem+$ lvPractitioners�� �� *
Thursday+$ OneWay=��� -"   $ 	Fridaychk��$��Friday��$
��Left=�$��
62,411,0,0q�$	��Top=�$��10(�$��Segoe UI Semibold)�$��17��$��74��$��2�� �� *SelectedItem+$ lvPractitioners�� �� *Friday+$ OneWay=��� -#   $ Saturdaychk��$��Saturday��$
��Left=�$��
62,443,0,0q�$	��Top=�$��10(�$��Segoe UI Semibold)�$��17��$��74��$��2�� �� *SelectedItem+$ lvPractitioners�� �� *
Saturday+$ OneWay=��� -$   $ 	Sundaychk��$��Sunday��$
��Left=�$��
62,475,0,0q�$	��Top=�$��10(�$��Segoe UI Semibold)�$��17��$��74��$��2�� �� *SelectedItem+$ lvPractitioners�� �� *Sunday+$ OneWay=��� -%   $ Homeno1��$
��Left=�$��24��$��256,381,0,0q�$
 Wrap=�$	��Top=�$	��178��$��Segoe UI Semibold)�$��10(�$��2�� �� *SelectedItem+$ lvPractitioners������ *	Homephone+$ OneWay=��� -&   $ 	Mobileno1��$
��Left=�$��25��$��256,410,0,0q�$
 Wrap=�$	��Top=�$	��178��$��Segoe UI Semibold)�$��10(�$��2�� �� *SelectedItem+$ lvPractitioners������ *Mobilephone+$ OneWay=��� $
��Left=�$��18��$��160,385,0,0q�$
��Wrap=�$	��Top=�$��85��$��Segoe UI Semibold)�$��10(�$��2���� $�Telephone(H���� $�ome):���� $
��Left=�$��18��$��156,412,0,0q�$
��Wrap=�$��Telephone(Mobile):��$	��Top=�$��91��$��Segoe UI Semibold)�$��10(�$��2���� -'   $ prac_gender��$
��Left=�$��24��$��255,209,0,0q�$	��Top=�$	��178��$��10(�$��2�� �� *SelectedItem+$ lvPractitioners�� �� *Gender+$ OneWay=�.@�	� $ Press=�$
��Male��	� $ Press=�$��Female��	� $ Press=�$��Other���� $
��Left=�$��16��$��196,223,0,0q�$
��Wrap=�$��Gender:��$	��Top=�$��41��$��Segoe UI Semibold)�$��10(�$��2���� -(   $  cboFieldNames_Practitioner��$
��Left=�$��476,94.333,0,0q�$	��Top=�$	��185��$��15��$��2���� -)   $" txtSearchString_Practitioner��$
��Left=�$��23��$��
699,94,0,0q�$
 Wrap=�
���   �$	��Top=�$	��166��$��16��$��2���� -*   $ fname1��$
��Left=�$��24��$��251,33.667,0,0q�$
 Wrap=�$	��Top=�$	��178��$��Segoe UI Semibold)�$��10(�$��1�� �� *SelectedItem+$ lvPractitioners������ *	FirstName+$ OneWay=��� -+   $ lname1��$
��Left=�$��24��$��251,61.667,0,0q�$
 Wrap=�$	��Top=�$	��178��$��Segoe UI Semibold)�$��10(�$��1�� �� *SelectedItem+$ lvPractitioners������ *
LastName+$ OneWay=��� -,   $
 MRNo��$
��Left=�$��24��$��251,6.667,0,0q�$
 Wrap=�$	��Top=�$	��178��$��Segoe UI Semibold)�$��10(�$��1�� �� *SelectedItem+$ lvPractitioners������ *
MedRegNo+$ OneWay=��� --   $ prac_address��$
��Left=�$��24��$��251,175.667,0,0q�$
 Wrap=�$	��Top=�$	��178��$��Segoe UI Semibold)�$��10(�$��1�� �� *SelectedItem+$ lvPractitioners������ *StreetAddress+$ OneWay=��� -.   $ prac_suburb��$
��Left=�$��24��$��251,204.667,0,0q�$
 Wrap=�$	��Top=�$	��178��$��Segoe UI Semibold)�$��10(�$��1�� �� *SelectedItem+$ lvPractitioners������ *Suburb+$ OneWay=��� -/   $ prac_postcode��$
��Left=�$��24��$��251,260.667,0,0q�$
 Wrap=�$	��Top=�$	��178��$��Segoe UI Semibold)�$��10(�$��1�� �� *SelectedItem+$ lvPractitioners������ *
PostCode+$ OneWay=�  -0   $ 	prac_date��$
��Left=�$��24��$��251,147.667,0,0q�$	��Top=�$	��178��$��10(�$��1�� �� *SelectedItem+$ lvPractitioners�� �� *DateOfBirth+$ OneWay=��� $
��Left=�$��18��$��183,41.667,0,0q�$
��Wrap=�$��First Name:��$	��Top=�$��67��$��Segoe UI Semibold)�$��10(�$��1���� $
��Left=�$��14��$��184,70.667,0,0q�$
��Wrap=�$��
Last Name:��$	��Top=�$��54��$��Segoe UI Semibold)�$��10(�$��1���� $
��Left=�$��14��$��188,268.667,0,0q�$
��Wrap=�$��
Post code:��$	��Top=�$��49��$��Segoe UI Semibold)�$��10(�$��1���� $
��Left=�$��16��$��98,8.667,0,0q�$
��Wrap=�$"��Medical Registration Number:��$	��Top=�$	��140��$��Segoe UI Semibold)�$��10(�$��1���� $
��Left=�$��14��$��165,190.667,0,0q�$
��Wrap=�$��Street Address:��$	��Top=�$��73��$��Segoe UI Semibold)�$��10(�$��1���� $
��Left=�$��14��$��199,219.667,0,0q�$
��Wrap=�$��Suburb:��$	��Top=�$��41��$��Segoe UI Semibold)�$��10(�$��1���� $
��Left=�$��14��$��174,159.667,0,0q�$
��Wrap=�$��Date of Birth:��$	��Top=�$��65��$��Segoe UI Semibold)�$��10(�$��1���� $
��Left=�$��14��$��209,243.667,0,0q�$
��Wrap=�$��State:��$	��Top=�$��28��$��Segoe UI Semibold)�$��10(�$��1���� -1   $ 
prac_state��$
��Left=�$��24��$��251,232.667,0,0q�$
 Wrap=�$	��Top=�$	��178��$��Segoe UI Semibold)�$��10(�$��1�� �� *SelectedItem+$ lvPractitioners������ *State+$ OneWay=��� -2   $ prac_ID��$
��Left=�$��24��$��251,152,0,0q�$
 Wrap=�$	��Top=�$	��178��$��Segoe UI Semibold)�$��10(�$��2�� �� *SelectedItem+$ lvPractitioners������ *	Prac_Id+$ OneWay=��� $
��Left=�$��16��$��167,157,0,0q�$
��Wrap=�$��Practitioner ID:��$	��Top=�$��69��$��Segoe UI Semibold)�$��10(�$��2���� $
��Left=�$��14��$��209,99.667,0,0q�$
��Wrap=�$��Desc:��$	��Top=�$��30��$��Segoe UI Semibold)�$��10(�$��1��$ 0.5,0.5&��� -3   $ 	prac_desc��$
��Left=�$��24��$��251,89.667,0,0q�$
 Wrap=�$	��Top=�$	��178��$��Segoe UI Semibold)�$��10(�$��1�� �� *SelectedItem+$ lvPractitioners������ *Desc+$ OneWay=��� $��Appointment Booking��$��-2,-2,1.333,0q�$��20��$	��Top=�.��� ���	#FFE5E5E5$��
-2,-60,2,0q�$	��700��$	��Top=�.+��� $
��Left=�$��0��$	��239��$	��Top=�$��	11,65,0,0q�$
��1031�� �� .P��� $��Doctor Name�� ��  �� Path$ Pilot_LicenceID��� $��Patient Name�� �� $ Pilot_FName��� $
��Date�� �� $ Pilot_SName��� $
��Time�� �� $ 	Pilot_DOB�$	 
dd/MM/yyyy��� $��	0,198,0,0q�$	��348��$	��Top=� �� $	��45* ��� $	��71* � �� $��1* ��� $��2* ��� $��2* �.+��� -4   $
 Addappt��$	��Add��$
��Left=�$��33��$��289.667,80.667,0,0q�$	��Top=�$��82��$��Segoe UI Semibold)�$��10(�$��1��$��1���� -5   $
 
Deleteappt��$��Delete��$
��Left=�$��33��$��289.667,162.667,0,17q�$��82��$��Segoe UI Semibold)�$��10(�$��1��$��1���� $
��Left=�$��15��$��130,136.667,0,0q�$
��Wrap=�$	��Top=�$��78��$��Segoe UI Semibold)�$��10(�$��1���� $�
Select a P���� $�atient���� $
��Left=�$��28��$��59,209.667,0,-25q�$	��Top=�$	��176��$��10(�$��1��$��1��.@��� $
��9.00���� $
��9.15���� $
��9.30���� $
��9.45���� $��10.00���� $��10.15���� $��10.30���� $��10.45���� $��11.00���� $��11.15���� $��11.30���� $��11.45���� $
��1.00���� $
��1.15���� $
��1.30���� $
��1.45���� $
��2.00���� $
��2.15���� $
��2.30���� $
��2.45���� $
��3.00���� $
��3.15���� $
��3.30���� $
��3.45���� $
��4.00���� $
��4.15���� $
��4.30���� $
��4.45���� $
��Left=�$��59,81.667,0,102q�$	��177��$��10(�$��1��$��1���� $
��Left=�$��14��$��140,217.667,0,-19q�$
��Wrap=�$��Select a Time��$	��Top=�$��69��$��Segoe UI Semibold)�$��10(�$��1���� $
��Left=�$��14��$��141,173.667,0,0q�$
��Wrap=�$��Select a Date��$	��Top=�$��66��$��Segoe UI Semibold)�$��10(�$��1���� -6   $
 
UpdateAppt��$��Update��$
��Left=�$��289.667,120.667,0,59q�$��82��$��Segoe UI Semibold)�$��10(�$��1��$��1��  $
��Left=�$��59,166.667,0,0q�$	��Top=�$ -0.279,0.77&�$	��179��$��33��$��Segoe UI Semibold)�$��10(�$��1��$��1���� $
��Left=�$��29��$��59,129.667,0,0q�$	��Top=�$	��177��$��10(�$��1��$��1���� $
��Left=�$��16��$��104,89.667,0,0q�$
��Wrap=�$��Select a Practiotioner��$	��Top=�$	��104��$��Segoe UI Semibold)�$��10(�$��1���� -7   $ cboFieldNames_Copy1��$
��Left=�$��286.667,36.667,0,0q�$	��Top=�$	��128��$��15��$��1��$��23��$��1���� -8   $ txtSearchString_Copy1��$
��Left=�$��23��$��28.333,33.667,0,0q�$
 Wrap=�
���   �$	��Top=�$	��146��$��16��$��2��$��1���� -9   $
 	ClearAppt��$��Clear��$
��Left=�$��33��$��289.667,204.667,0,-25q�$	��Top=�$��82��$��Segoe UI Semibold)�$��10(�$��1��$��1��$ -0.537,-0.161&��� -:   $
 	btnSearch��$��Search��$
��Left=�$��849,364,0,0q�$	��Top=�$��75��$��17��