   M S B A M L   `   `   `  ����B  >ZeroApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ZeroApp.Windows.DialogsZeroApp.Windows.Dialogs  5   	   ,    &ZeroApp.Windows.Dialogs.Dialog_AddRepo   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:ZeroApp.Windows.Dialogs     5   	     � Title$%  Zero - Přidat balíček módů��5   	   $��211.2��6,   $	��450��6;    � WindowStyle$
 None=�6G    � 
ResizeMode$ NoResize=�6Z    � WindowStartupLocation$ CenterOwner=�6p   .��5	      � .+�5
   
   � -   5         �Name$ Dialog_Headbar��5
      $��22��6'   $	��Top=�63   
���   �.+�5      �� -   5         ��Name$ WindowActionPanel��5       �� Orientation$ 
Horizontal=�64   $��28��6M   $��Right=�6X   .+�5      �� -   5         ��Name$ Button_ExitDialog��5      
������ ���	#FED8D8D8$��28��6d   $��22��6o   ��6{   Z� ��5      �� 5       �� 
TargetType" ��6*   .R�5      � $��AppExitButtonGrid��6#   
������ .+�5   "   �� $��12��6(   $��12��64   $4��.pack://application:,,,/.zeroapp/media/quit.png��6?   5      	 �� Triggers	 5      P� 5   "   
 P� Property
 �@J�IsMouseOver P� Value . . �5   &   ��  �� 
TargetName$ AppExitButtonGrid��6C    �� Property � �� �� Value �#F047476b   5   #   5      5      5      �� 5      $��Přidat balíček módů��6   ���	#FF7F7F7F$	��119��6I   $��22��6U   $
��Left=�6a   $	��Top=�6|   $��10,0,0,0q�6�   $��10(�6�   5      � 5   
   -   5        $ Dialog_Content��5      $��0,22,-0.4,0q�6'   ���#141414.+�5      �� -   5         ��Name$ Input_SourceRepository��5      $��14,67,13.6,0q�66   $��50��6L   ���	#FF4F4F4F���	#FFC5C5C5$��	20,17,0,0q�6�   $	��Top=�6�   ��6�   Z� 6�   �� 5       $��Zdrojové URL��6   
�������$	��154��6>   $��24��6J   $
��Left=�6V   $	��Top=�6q   $��	10,45,0,0q�6�   $
��Bold"�6�   �� 5"      -   5        $ Button_Continue��5"      $��311,126,14,12.8q�6.   ��5#      �� 5$      " ��6&   .R�5%      � $ AuthLoginButtonGrid��6   ���#202020 � Cursor$
 Hand��6Q   .+�5&      ��  ��Name$ AuthLoginTextBlock��6(   $��PŘIDAT��6D   ���#FEFEFE$
��Bold"�6g   $��16(�6y   $��Center=�6�   $��Center=�6�   5'      	 5(      P� 5)      
 �@J�IsMouseOver . . �5*   "   �� $ AuthLoginButtonGrid��6?    � �� �#3232326`   5+      P� 5,      
 � �� . . �5-   "   �� $ AuthLoginTextBlock��6?    � �� �#FFF6_   �� 5.   "   $ AuthLoginButtonGrid��6?    � �� �	#FF5680586`   5/      51      53      55      56      57      