   M S B A M L   `   `   `  ����B  >ZeroApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:ZeroAppZeroApp  5   	       ZeroApp.AuthWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:ZeroApp     5   	     � Title$  
AuthWindow��5   	   $	��450��6   $	��400��6)   ���	#FF121212���	#FFFEFEFE � AllowsTransparency .  � WindowStyle$
 None=�6}    � WindowStartupLocation$ CenterOwner=�6�   .��5	      � .+�5
   
   � -   5         �Name$ Dialog_Headbar��5
      $��22��6'   $	��Top=�63   ���	#FF3B9363.+�5      �� $��Zero - Ověření člena��6   
�������$	��119��6F   $��22��6R   $
��Left=�6^   $	��Top=�6y   $��10,0,0,0q�6�   $��10(�6�   �� 5      -   5         ��Name$ WindowActionPanel��5       �� Orientation$ 
Horizontal=�64   $��28��6M   $��Right=�6X   5      5      � 5    
   -   5        $ WindowContentGrid��5       $��0,22,0,0q�6*   .+�5!      �� $��18(�6    �� TextAlignment$ Center=�6&   $��0,83,0,307.6q�6=   $
��Bold"�6S   �� 6f   $�Ověření člena��6j   6�   �� 5#      -   5         ��Name$ LoginUsernameInput��5#      $	��300��62   $��Center=�6>   ���	#FF4F4F4F���	#FFC5C5C5$��	20,17,0,0q�6�   $��50��6�   $��50,158,50,219.6q�6�   ��6�   Z� 6�   G� 5$      -   5        	 G�Name$	 LoginUserPinInput��5$      $	��300��65   $��Center=�6A   ���	#FF4F4F4F���	#FFC5C5C5$��	20,17,0,0q�6�   $��50��6�   $��50,230,50,147.6q�6�   ��6�   Z� 6�   �� 5&      $��12(�6   $
 Left=�6&   $	��100��6;   $��12��6G   $
��Bold"�6S   $��50,141,250,274.6q�6e   �� 6�   $�NICKNAME��6�   6�   �� 5'      $��12(�6   $
 Left=�6&   $	��100��6;   $��12��6G   $
��Bold"�6S   $��50,213,250,202.6q�6e   �� 6�   $�HESLO��6�   6�   �� 5)      -   5        
 ��Name$
 LoginActionButton��5)      ���	#FF151515$	��300��6G   $��50��6S   $��PŘIHLÁSIT SE��6v   $��14(�6�   ���	#FED8D8D8$��Center=�6�   $
��Bold"�6�   $	��300��6�   $��50��6�   $��50,305,50,72.6q�6�   ��6_   Z� ��5*      �� 5+       �� 
TargetType" ��6&   .R�5,      � $ AuthLoginButtonGrid��6   ���#242424 � Cursor$
 Hand��6Q   .+�5-      ��  ��Name$ AuthLoginTextBlock��6(   $��PŘIHLÁSIT SE��6D   $��16(�6X   $��Center=�6f   $��Center=�6�   5.       �� Triggers 5/      P� 50       P� Property �@J�IsMouseOver P� Value . . �51   "   ��  �� 
TargetName$ AuthLoginButtonGrid��6?    �� Property � �� �� Value �#3232326`   52      P� 53       � �� . . �54   "   �� $ AuthLoginTextBlock��6?    � �� �#FFF6_   �� 55   "   $ AuthLoginButtonGrid��6?    � �� �	#FF5680586`   56      58      5:      �� 5<      -   5        $ NoAccountLabel��5<      $��10(�60   $
 Left=�6>   $��14��6S   $��197,382,143,31.6q�6_   �� 6z   $�Nemáš účet?��6~   6�   �� 5=      -   5        $ NoAccountLabelLink��5=      $
 Left=�64   ���	#FE80A6FF$��10(�6`   $��270,382,50,31.6q�6�   ��6n   Z� �� 6�   $�Kontaktuj správce��6�   6�   �� 5>      -	   5         ��Name$ CheckBox_RememberMe��5>      $
��Left=�64   $��114,382,0,0q�6O   $	��Top=�6d   
�������
�������$��14��6�   
���   �6�   �� 5?      -
   5        $ RememberMe_Copy��5?      $��10(�61   $
 Left=�6?   $��14��6T   $��50,382,290,31.6q�6`   �� 6z   $�Zapamatovat��6~   6�   5A      5B      5C      