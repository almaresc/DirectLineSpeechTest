   M S B A M L   `   `   `  ����E  Awpc-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:wpc_client
wpc_client  5   	       wpc_client.MainWindow   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:wpc_client     5   	     � Title$  Texa PoC��5   	   $	��450��6   $	��800��6'   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��50 �6    � RowDefinitions 5   
   �� 5      $��40 �6   6+   �� 5      $��50 �6   6+   �� 5      $��50 �6   6+   �� 5      $��50 �6   6+   �� 5      $��* �6   6*   .+�5   
   �� -   5         ��Name$ micBtn��5   &   $��2��5      $��4��6!   $��Center=�6.   $��Bottom=�6K   
�������.��5      �� $��resources/microphone.png��6   $
��Fill=�66   $��50��6E   $��50��6Q   $��Center=�6\   $��Center=�6w    J� RenderTransformOrigin$ 0.5,0.5&�6�    � UseLayoutRounding$
 True��6�   5      �� 5   
   -   5         ��Name$ 
inprogress��5   c   $��0��6   $��0��6#   $
��Left=�60   $	��Top=�6K   .��5      �� $#��Simulate Analysis in progress��6   $��10(�6=   
���   �6K   5      �� 5!   
   -   5        $ 	reconnBtn��5"   )   $��2��5!      $��2��6!   $��Center=�6.   $	��Top=�6K   
��� d �$��50��6z   $��50��6�   .��5#      �� $��Re-Conn��6   $��10(�6'   
�������65   5$      �� 5'   
   $��2q�6   
��� � �$��0��69   $��2��6I   $��2��5(      $��3��6(   ._�5)      �� .��5*      �� -   5         ��Name$ 	Notifiche��5+   (   $��Nothing to execute��5*      $��20(�62   $
��Auto=�6@   
��� � �5+   =   5,      5-      5/      50      