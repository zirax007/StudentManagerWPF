   M S B A M L   `   `   `  ����L  HStudentManagerWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:StudentManagerWPFStudentManagerWPF  5   	   "    StudentManagerWPF.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:StudentManagerWPF     5   	   b ^MaterialDesignThemes.Wpf, Version=3.2.0.1979, Culture=neutral, PublicKeyToken=df2a72020bd7962af bMaterialDesignThemes.MahApps, Version=0.1.5.1979, Culture=neutral, PublicKeyToken=df2a72020bd7962aHmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes   5   	   T PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3f6mah,http://metro.mahapps.com/winfx/xaml/controls  5   	   -   5          � Icon$  Assets/images.png��5
   	    � Title$ LoginWindow��5   	   $	��450��6   $	��800��6*    � 
ResizeMode$ NoResize=�66    � WindowStartupLocation$ CenterScreen=�6L    � WindowStyle$
 None=�6q   .��5      �  � RowDefinitions 5   
   �� 5      $	��40* �6   �� 5      .+�5   
   �� $	��307��6   $	��Top=�6!   ��5      �� 5       �� EndPoint$ 0.5,1&�6&    �� 
StartPoint$ 0,0.5&�67   .��5      �  � Color$ #F7FDFE��6#   	 � Offset$		 1.5Q�63   � 5      $ #A3CBD0��6#   $	 .5Q�63   5      5      � 5   
   $��2��6    5      �� 5      $��* �6    6-   �� 5      $��7* �6    6.   �� 5      $��* �6    6-   .+�5      �� -   5        $��Assets/shut.png��5      $��0��6-   $��15��6:   $��15��6F   $��Right=�6Q   $	��Top=�6�   $��	0,10,10,0q�6�   �� 5       -   5        $��Assets/mini.png��5       $��0��6-   $��15��6:   $��15��6F   $��Right=�6u   $	��Top=�6�   $��	0,10,40,0q�6�   5!      � 5"   
   $��Center=�6   $	��450��6,   $��116,0q�69   $��2��6H   .+�5#      �� $��Assets/ensa-safi-.png��6   $	��Top=�63   $	��-20q�6K   $	��600��6X   $	��189��6d   �� 5$      $��Gestion Des Etudiants��6   $��Center=�64   
���   �$��24(�6d   $
��Bold"�6r   $
��auto��6�   $
��auto��6�   $��159,120,154,281q�6�   �� 5&      $	��514��6   $	��190��6%   
 �� RadiusY$
 12��63    �� RadiusX$ 12��6@   
�������$��Bottom=�6Z   $
��0,70q�6u   $��Center=�6�    J� Effect 5'      3  -System.Windows.Media.Effects.DropShadowEffect  5(         Color$ 	#FFC1D3E4��6'      	Direction$ 0Q�69      
BlurRadius$ 15Q�6G      RenderingBias$ Quality=�6W      ShadowDepth$ 1Q�6o   5*      � 5+      $	��514��6   $	��190��6   $��Bottom=�6-   $
��0,70q�6H   $��Center=�6V    � ColumnDefinitions 5,      �� 5-      $
��275* �6'   �� 5.      $
��239* �6'   .+�51      �� $��Center=�6   $��10,16,10,46q�68   $��2��6M   .+�52      �� -   5         ��Name$ error��52      $��Center=�6+   $	��Top=�6I   ���	#FFFF06066a   �� 53      -   5         ��Name$ username��53      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Username��60    %  !MaterialDesignFloatingHintTextBox#��[  6Z   $	��458��6�   $
��0,10q�6�   G� 54      -   5         G�Name$	 mdp��54   "   $ Password��6/    ) %MaterialDesignFloatingHintPasswordBox#��[ 6Y   $	��458��6�   $
��0,10q�6�   55      56      �� 57      -   5         ��Name$ login��57      $��LOGIN��6$   $��Bottom=�6J   $��48��6e   ���	#FFCDE4E7$��45q�6�   $
��Bold"�6�   $��16(�6�   $��Center=�6�   $	��116��6�   
���   � � 	Resources 58       �� 
TargetType �� Property �� Value��%^   '��       �� 59      " ��6   .�5:      ��  �@��CornerRadius$ 8��69   5;      5<      5=      5>      5B      5C      