   M S B A M L   `   `   `  ����X  TFreight_transportation_system, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:Freight_transportation_systemFreight_transportation_system  5   	   2    ,Freight_transportation_system.AddOrderWindow   5   	   _ [MaterialDesignThemes.Wpf, Version=5.2.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   =local+clr-namespace:Freight_transportation_system     5   	     � Title$  AddOrderWindow��5	   	   $	��720��6    $
��1080��6-    � WindowStartupLocation$ CenterScreen=�6:    � 	Resources 5         MyStyledTextBox �� 
TargetType % !MaterialDesignFloatingHintTextBox �� BasedOn �� Property �� Value  MyStyledTextBoxSecond��%�  &	        7  &	 D    7  �� 5   
   " ��6(   8   .�5      ��  � �� �#FFF0F36+   �� 5       � ��$ 15(�6)   �� 5       � ��$ SemiBold"�6+   �� 5       � �� �#590D226+   �� 5       � ��$ 0q�60   �� 5       � ��$ 
10 15 10 5q�6(   �� 5       � ��$ Segoe UI)�6+   5      �� 5   
   " ��6.   8   .�5      ��  � �� �#FFF0F36+   �� 5       � ��$ 15(�6)   �� 5       � ��$ SemiBold"�6+   �� 5       � �� �#FF8FA36+   �� 5       � ��$ 0q�60   �� 5       � ��$ 15q�6(   �� 5       � ��$ Segoe UI)�6+   5      5      .��5!      �� -   5        ���#caf0f8._�5#   
   �  � ColumnDefinitions 5$      �� 5%      $	��400 �6#   �� 5&      $��* �6#   �� 5'      $��* �6#   .+�5*      �� $��0��6   ���#c9184a �� CornerRadius$ 	0,80,80,0��5,      ._�5.      � 	 � RowDefinitions	 5/      �� 50      $
��Auto �6(   .+�56      �� $��0��6   
 �� Orientation$
 Vertical=�57      $��30 50 30 40q�58      .+�5:      �� $��ImagesIcon/user.png��6!   $��80��5;   "   $��80��5<   "   $��10q�5=   "   5?      �� 5A      $��ПРО СЕБЕ��6$   ���#FAFAFF$��30(�5C   $   $��SemiBold"�5D   $   $��Segoe UI)�5E   $    �� Padding$ 10q�5F   $   �� 5H      $%��Ім'я користувача:��6$   ���#FAFAFF$��25(�5J   %   $ 10q�5K   %   $��Segoe UI)�5L   %   �� 5O      #��[  6"   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$  Поле для вводу��5Q   "   .  (MaterialDesignThemes.Wpf.TextFieldAssist   DecorationVisibility$ Visible=�5R   "      UnderlineBrush
 �������5P   "   �� *
UserName+ �� UpdateSourceTrigger$ PropertyChanged=�5S   "   �� 5T      ���#FFF0F3$��12(�6X   $��10,2,0,0q�6f   ��6$   �� *UserNameError+6f   �� 5V      $��Прізвище:��6$   ���#FAFAFF$��25(�5X   $   $ 10q�5Y   $   $��Segoe UI)�5Z   $   �� 5\      #��[  6#   $  Поле для вводу��5^   #   $ Visible=�5_   #   
 �������5]   #   �� *
LastName+$ PropertyChanged=�5`   #   �� 5a      ���#FFF0F3$��12(�6X   $��10,2,0,0q�6f   ��6$   �� *LastNameError+6f   �� 5b      $"��Номер телефону:��6$   ���#FAFAFF$��25(�5d   $   $ 10q�5e   $   $��Segoe UI)�5f   $   �� 5h      #��[  6#   $  Поле для вводу��5j   #   $ Visible=�5k   #   
 �������5i   #   �� *PhoneNumber+$ PropertyChanged=�5l   #   �� 5m      ���#FFF0F3$��12(�6[   $��10,2,0,0q�6i   ��6$   �� *PhoneNumberError+6i   �� 5o      $��ImagesIcon/sedan.png��6"   $	��150��5p   "   $	��150��5q   "   $��10q�5r   "   5t      5w      5x      �� 5z      $��1��6   $
 Vertical=�5{      $��20 30 10 30q�5|      .+�5      ��  �� DisplayMemberPath$
 Name��5�      $	��250��5�      $��30��5�      $��10 30 10 10q�5�      $&  🚚 Тип транспорту��5�      ���#FF4D6D
�������$
��Bold"�5�      $��5q�5�      ��5      �� *TransportTypes+ �� SelectedItem 5�      �� *SelectedTransportOption+ �� Mode$ TwoWay=���5�      �� 5�      " ��6    .�5�      ��  � �� �#800F2F6;   �� 5�       � ��
 �����6;   5�      5�      �� 5�      $��ВАНТАЖ��6   ���#590D22$��18(�5�   $   $
��Bold"�5�   $   $��Segoe UI)�5�   $   $��Center=�5�   $   �� 5�      $ 	CargoType��5�      $	��250��5�      $��30��5�      $��10 10 q�5�      $ Вантаж��5�      ���#FF4D6D
�������$
��Bold"�5�      $��5q�5�      ��5�      �� *	CargoType+ 5�      �� *SelectedCargoType+$ TwoWay=���5�      �� 5�      " ��6    .�5�      ��  � �� �#800F2F6;   �� 5�       � ��
 �����6;   5�      5�      �� 5�      $��	ВАГА:��6   ���#590D22$��15(�5�   $   $
��30 0q�5�   $   $��Segoe UI)�5�   $   $��SemiBold"�5�   $   �� 5�      #��[ 6   $
��30 0q�5�      $��
10 15 10 5q�5�      $��14(�5�      $  Поле для вводу��5�      $ Visible=�5�      
 �������5�      �� *
WeightText+$ TwoWay=�$ PropertyChanged=�5�      �� 5�      ���#800F2F$��12(�6N   $��10,2,0,0q�6\   ��6   �� *WeightError+6\   �� 5�      $��
ОБ'ЄМ:��6   ���#590D22$��15(�5�      $
��30 0q�5�      $��Segoe UI)�5�      $��SemiBold"�5�      �� 5�      #��[ 6   $
��30 0q�5�      $��
10 15 10 5q�5�      $��14(�5�      $  Поле для вводу��5�      $ Visible=�5�      
 �������5�      �� *
VolumeText+$ TwoWay=�$ PropertyChanged=�5�      �� 5�      ���#800F2F$��12(�6N   $��10,2,0,0q�6\   ��6   �� *VolumeError+6\   �� 5�      $&�� СПЕЦІАЛЬНІ УМОВИ:��6    ���#590D22$��15(�5�      $
��30 0q�5�      $��Segoe UI)�5�      $��SemiBold"�5�      �� 5�      $ ConditionType��5�      $	��250��5�      $��30��5�      $��10 5 q�5�      $ 
Умови��5�      ���#FF4D6D
�������$
��Bold"�5�      $��5q�5�      ��5�      �� *ConditionType+ 5�      �� *SelectedConditionType+$ TwoWay=���5�      �� 5�      " ��6    .�5�      ��  � �� �#800F2F6;   �� 5�       � ��
 �����6;   5�      5�      �� 5�      $��МАРШРУТ��6   ���#590D22$��18(�5�      $
��Bold"�5�      $��Segoe UI)�5�      $��Center=�5�      � 5      5     �� 5     $��* �6+   �� 5     $��* �6+   	 5     �� 5     $��* �6(   �� 5	     $��* �6(   .+�5     �� $��0��6"   $��0��5  "   $��ВІДПРАВЛЕННЯ��5  "   $��Center=�5  "   ���#590D22$��10(�5  "   $��0q�5  "   $��Segoe UI)�5  "   $��SemiBold"�5  "   �� 5     $��1��6"   $��0��5  "   $��ПРИБУТТЯ��5  "   $��Center=�5  "   ���#590D22$��10(�5  "   $��0q�5  "   $��Segoe UI)�5  "   $��SemiBold"�5  "   �� 5      $��0��6"   $��1��5!  "   $ CitiesOfDeparture��5$  "   $
��Auto��5&  "   $��30��5'  "   $��10 5 q�5(  "   $ З��5)  "   ���#FF4D6D
�������$
��Bold"�5-  "   $��5q�5.  "   ��5"  "   �� *CitiesOfDeparture+ 5#  "   �� *SelectedDepartureCity+$ TwoWay=���50     �� 51     " ��6$   .�52  "   ��  � �� �#800F2F6?   �� 53  "    � ��
 �����6?   54     57     �� 59     $��1��6#   $��1��5:  #   $ CitiesOfArrival��5=  #   $
��Auto��5?  #   $��30��5@  #   $��10 5 q�5A  #   $
 До��5B  #   ���#FF4D6D
�������$
��Bold"�5F  #   $��5q�5G  #   ��5;  #   �� *CitiesOfArrival+ 5<  #   �� *SelectedArrivalCity+$ TwoWay=���5I     �� 5J     " ��6$   .�5K  "   ��  � �� �#800F2F6?   �� 5L  "    � ��
 �����6?   5M     5P     5Q     5V     �� 5X     $��2��6   $
 Vertical=�5Y     $��20 30 10 30q�5Z     .+�5\     �� -   5         ��Name$
 Save��5\     $��Зберегти��6'      MaterialDesignOutlinedButton#��[ 5]     $
��20,5q�5^     $��15(�5_     $
��Bold"�5`     ���#FF4D6D5b     5c     5e     5g     5i     