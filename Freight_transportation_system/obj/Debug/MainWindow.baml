   M S B A M L   `   `   `  ����X  TFreight_transportation_system, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:Freight_transportation_systemFreight_transportation_system  5   	   .    (Freight_transportation_system.MainWindow   5   	   _ [MaterialDesignThemes.Wpf, Version=5.2.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   =local+clr-namespace:Freight_transportation_system     5   	     � Title$  
MainWindow��5	   	   $	��800��6   $
��1080��6)    � WindowStartupLocation$ CenterScreen=�66    � 	Resources 5         MyDividerBrush �� Color��%+   &	        �� 5   
   $ #273469��61   5      .��5      �� -   5        ���#caf0f8._�5   
   �  � ColumnDefinitions 5      �� 5      $	��250 �6#   �� 5      $��* �6#   .+�5      �� ���#c9184a �� CornerRadius$ 	0,80,80,0��5      $	��600��5      $��	0,40,0,40q�5      ._�5      � $��0,0,0,10q�6    � RowDefinitions 5       �� 5!      $
��Auto �6(   �� 5"      $��* �6(   �� 5#      $
��Auto �6(   .+�5&      �� $��0��6!   $��Center=�5'   !   $��Center=�5(   !   $��0,40,0,0q�5)   !   .+�5,      �� $��0��6    $��ImagesIcon/sedan2.png��5-   !   $	��150��5.   !   $	��150��5/   !   $��0,0,0,50q�51   !   �� 56      $��FREIGHT��6$   $��1��57   $   ���#FAFAFF$��25(�59   $   $��SemiBold"�5:   $    �� TextAlignment$ Center=�5;   $   $��Segoe UI)�5<   $   �� 5>      $��TRANSPORTATION��6$   $��1��5?   $   ���#FAFAFF$��25(�5A   $   $��SemiBold"�5B   $   $ Center=�5C   $   $��Segoe UI)�5D   $   �� 5F      $��
MANAGEMENT��6$   $��1��5G   $   ���#FAFAFF$��30(�5I   $   $��SemiBold"�5J   $   $ Center=�5K   $   $��Segoe UI)�5L   $   5N      �� 5S      $��2��6   $��ImagesIcon/sedan.png��5T   $   $	��150��5U   $   $	��150��5V   $   $��0,0,0,20q�5X   $   5Z      5\      � 5^      $��1��6   $��30,100,30,0q�6#    5_      �� 5`      $
��Auto �6$   �� 5a      $
��Auto �6$   �� 5b      $
��Auto �6$   �� 5c      $��* �6$   �� 5d      $
��Auto �6$   �� 5e      $
��Auto �6$   .+�5i      �� $��Замовлення��6   $��40(�5j      $��SemiBold"�5k      ���#30343F$��0 0 0 10q�5m      $��0��5n      �� 5r      -   5         ��Name$ AddOrder��5r      $��0��6+   $0��*ДОДАТИ НОВЕ ЗАМОВЛЕННЯ��5s      ���#FF758F   MaterialDesignOutlinedButton#��[ 5u      $��	0,10,0,10q�5v      $
��30,5q�5w      $��Right=�5x      $��15(�5y      $
��Bold"�5z      $	��150��5{      5|      �� 5      $��1��6   #���   5�      $
��0 10q�5�      �� 5�      $��2��6   	 �� Orientation$	 
Horizontal=�5�      $��0,10,0,0q�5�      .+�5�      �� $
��60 0q�6   $	��400��5�      $��40��5�      ���#5ec9c0$ 20��5�      ._�5�      �� -   5        
 ��Name$
 	SearchBox��5�   "   $	��350��5�      5�      �� 5�      -   5        $��
Пошук��5�      $	��100��5�      ���#FF758F$
��Bold"�5�      5�      &   System.Windows.Controls.DataGrid  5�      -   5          Name$ dataGridView1��5�      $��3��5�         AutoGenerateColumns .  $��	0, 10,0,0q�5�        MaterialDesignDataGrid#��[ 5�         RowHeaderWidth$ 0Q�5�         SelectionUnit$ FullRow=�5�         CanUserAddRows .  $��14(�5�      $��Segoe UI)�5�         GridLinesVisibility$	 All=�5�         HorizontalGridLinesBrush �#264653   VerticalGridLinesBrush �#264653   ColumnHeaderHeight$ 40Q�5�         	RowHeight$ 50Q�5�         SelectionMode$ Single=�5�         
IsReadOnly . ���#93e1d8$
��Auto=�5�      ��5�      �� *
TempOrders+ 5�      =  7System.Windows.Controls.Primitives.DataGridColumnHeader �� 
TargetType �� Property �� Value)  #System.Windows.Controls.DataGridRow �� Triggers P� Property P� Value , (MaterialDesignComboBoxDropdownBackground��%m  '        '  	    &	 .    �� 5�      "  6    .�5�      ��  � �� �#2646536;   �� 5�       � ��
 �����6;   �� 5�       � ��$ 14(�69   �� 5�       � ��$
 Bold"�6;   �� 5�       � ��$ Center=�6K   �� 5�       � ��$ 8q�68   5�      �� 5�      "  6    .�5�      ��  � ��
 ���� 6;   �� 5�       � ��
 �   �6;   �� 5�       � ��$ 14(�69    5�      P� 5�   "    �@ 
IsSelected . . �5�   &   ��  � �� �#FF758F6C   �� 5�   &    � ��
 �   �6C   5�   #   5�      �� 5�      $ #FF4D6D��6[   5�         Columns 5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,  &System.Windows.Controls.DataGridColumn   Header$! Дата створення��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6i   1  +System.Windows.Controls.DataGridBoundColumn    Binding  6E   �� *CreatedDateOnly+6i     5�      $ 
Номер��6-   $
 Auto 6W     6<   �� *Number+6W     5�      $ Транспорт��6-   $
 Auto 6^     6@   �� *	Transport+6^     5�      $ Відправлення��6-   $
 Auto 6a     6C   �� *	Departure+6a     5�      $ Прибуття��6-   $
 Auto 6[     6?   �� *	Arrival+6[     5�      $ Сума��6-   $
 Auto 6S     6;   �� *Sum+6S   4  .System.Windows.Controls.DataGridTemplateColumn  5�      $# Статус доставки��61   $	 150 6J   !   CellTemplate! 5�      �� 5�   "   .R�5�   &   �� $
��Auto��5�   /   ���#FF8FA3
�������$
��Bold"�5�   /   $��5q�5�   /   ��5�   /   �� *DeliveryStatusList+" �� SelectedItem" 5�   /   �� *DeliveryStatus+# �� Mode$# TwoWay=�$ �� UpdateSourceTrigger$$ PropertyChanged=���5�   *   �� 5�   .   " ��64   .�5�   2   ��  � �� �#c9184a6O   �� 5�   2    � ��
 �����6O   5�   /   5�   '   5�   #   5�      5�      �� 5�      $	 
Horizontal=�6   $��4��66   $��0q�6C   ���#264653.+�5�      �� $��16(�5�       $
��Bold"�5�       $��10q�5�       
���������5�       �� *TotalSumText+5�       5�      �� 5�      $	 
Horizontal=�6   $��5��66   $��	0,50,0,10q�6C   .+�5     �� -   5        $ SaveOrdersButton��5     $��Зберегти��5     $��	0,0,20,60q�5     $	��150��5     $
��Left=�5     $��Bottom=�5     ���#800F2F
�������$��15(�5     $
��Bold"�5     5     �� 5     -   5        $ DeleteButton��5     $��Видалити��5     $��Bottom=�5     $
��Left=�5     $��
5,0,100,60q�5     $	��150��5     ���#C9184A
�������$��15(�5     $
��Bold"�5     {�.  5     �� 5      -   5        $ DetailsButton��5!     $��Деталі��5"     $��	0,0,20,60q�5#     $	��150��5$     $
��Left=�5%     $��Bottom=�5&     #��[ 5'     ���#FF8FA3$��15(�5)     $
��Bold"�5*     {�.  5,     �� 50     -	   5        $ 
EditButton��51     $��Редагувати��52     $��Bottom=�53     $��Right=�54     $��	0,0,20,60q�55     $	��150��56     #��[ 57     ���#FF8FA3$��15(�59     $
��Bold"�5:     {�.  5<     5=     5?     5@     5B     5C     