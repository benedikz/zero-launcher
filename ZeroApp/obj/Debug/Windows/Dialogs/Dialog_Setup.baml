   M S B A M L   `   `   `  ����B  >ZeroApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ZeroApp.Windows.DialogsZeroApp.Windows.Dialogs  5   	   ?$clr-namespace:ZeroApp.Controls.ListsZeroApp.Controls.Lists  5   	   *    $ZeroApp.Windows.Dialogs.Dialog_Setup   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:ZeroApp.Windows.Dialogs     5   	   6Lists$clr-namespace:ZeroApp.Controls.Lists     5   	     � Title$  Zero - První spuštění��5	   	   $��211.2��6'   $	��450��66    � WindowStyle$
 None=�6B    � 
ResizeMode$ NoResize=�6U    � WindowStartupLocation$ CenterOwner=�6k   .��5
      � .+�5   
   � -   5         �Name$ Dialog_Headbar��5      $��22��6'   $	��Top=�63   
���   �.+�5      �� -   5         ��Name$ WindowActionPanel��5       �� Orientation$ 
Horizontal=�64   $��28��6M   $��Right=�6X   .+�5      �� -   5         ��Name$ Button_ExitDialog��5      
������ ���	#FED8D8D8$��28��6d   $��22��6o   $	��Top=�6�   ��6{   Z� ��5      �� 5       �� 
TargetType" ��6*   .R�5      � $ AppExitButtonGrid��6#   
������ .+�5   "   �� $��12��6(   $��12��64   $4��.pack://application:,,,/.zeroapp/media/quit.png��6?   5      	 �� Triggers	 5      P� 5   "   
 P� Property
 �@J�IsMouseOver P� Value . . �5   &   ��  �� 
TargetName$ AppExitButtonGrid��6C    �� Property � �� �� Value �#F047476b   5   #   5      5      5      �� 5      $#��Nastavení - Spouštění hry��6   ���	#FF7F7F7F$	��407��6O   $��22��6[   $
��Left=�6g   $	��Top=�6�   $��10,0,0,0q�6�   $��10(�6�   5      � 5   
   -   5        $ Dialog_Content��5      $��0,22,-0.4,0q�6'   ���#141414.+�5       �� -   5         ��Name$ Input_ApplicationLocation��5    (    �� 
IsReadOnly . $��
14,67,70,0q�6K   $��50��6_   ���	#FF4F4F4F���	#FFC5C5C5$��	20,17,0,0q�6�   $	��Top=�6�    �� IsReadOnlyCaretVisible . ��6�   Z� 6�   �� 5!      $��Cesta k aplikaci hry:��6   
�������$	��154��6G   $��24��6S   $
��Left=�6_   $	��Top=�6z   $��	10,45,0,0q�6�   $
��Bold"�6�   �� 5#      -   5        $ Button_Continue��5#      $��289,126,10,13.2q�6.   ��5$      �� 5%      " ��6&   .R�5&      � $ AuthLoginButtonGrid��6   ���#202020 � Cursor$
 Hand��6Q   .+�5'      ��  ��Name$ AuthLoginTextBlock��6(   $��POKRAČOVAT��6D   ���#FEFEFE$
��Bold"�6l   $��16(�6~   $��Center=�6�   $��Center=�6�   5(      	 5)      P� 5*      
 �@J�IsMouseOver . . �5+   "   �� $ AuthLoginButtonGrid��6?    � �� �#3232326`   5,      P� 5-      
 � �� . . �5.   "   �� $ AuthLoginTextBlock��6?    � �� �#FFF6_   �� 5/   "   $ AuthLoginButtonGrid��6?    � �� �	#FF5680586`   50      52      54      �� 56      -   5        $ Button_GetDirectory��56      $��385,67,10,72.2q�62   ��57      �� 58      " ��6&   .R�59      � $ GetDirectoryButtonGrid��6   ���#202020$
 Hand��6T   .+�5:      ��  ��Name$ GetDirectoryIcon��6s   $��Center=�6$   $��Center=�6?   $��32��6\   $��32��6g   $9��3pack://application:,,,/.zeroapp/media/magnifier.png��6�   5;      	 5<      P� 5=      
 �@J�IsMouseOver . . �5>   "   �� $ GetDirectoryIcon��6<    �@J�Opacity$	 0.5Q�6Z   �� 5?   "   $ GetDirectoryButtonGrid��6?    � �� �#3232326c   5@      P� 5A      
 � �� . . �5B   "   �� $ GetDirectoryButtonGrid��6?    � �� �	#FF5680586c   5C      5E      5G      5H      5I      5J      