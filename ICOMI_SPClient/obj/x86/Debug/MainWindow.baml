   M S B A M L   `   `   `  ����H  DMahApps.Metro, Version=0.7.1.0, Culture=neutral, PublicKeyToken=nullV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5       ICOMI_SPClient   ICOMI_SPClient.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5      c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   h dMicrosoft.Expression.Interactions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bei9http://schemas.microsoft.com/expression/2010/interactions  5      OOptionsAhttp://schemas.microsoft.com/winfx/2006/xaml/presentation/options  5      :d2http://schemas.microsoft.com/expression/blend/2008  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5	        � Title$  ICOMI : Carnet Sanitaire��5   	   (   "MahApps.Metro.Controls.MetroWindow   ShowIconOnTitleBar .  $	��768��5   	   $
��1366��6    � WindowStartupLocation$ CenterScreen=�6#   
������� � 	Resources 5       �� MergedDictionaries �� Source �� ImageSource   Key �� Opacity j� Stretch	 j� 
AlignmentX�� 5   
    5      �� 5      $H Bpack://application:,,,/MahApps.Metro;component/Styles/Colours.xaml?�6%   �� 5      $F @pack://application:,,,/MahApps.Metro;component/Styles/Fonts.xaml?�6%   �� 5      $I Cpack://application:,,,/MahApps.Metro;component/Styles/Controls.xaml?�6%   �� 5      $e _pack://application:,,,/MahApps.Metro;component/Styles/Controls.AnimatedSingleRowTabControl.xaml?�6%   �� 5      $I Cpack://application:,,,/MahApps.Metro.Resources;component/Icons.xaml?�6%   �� 5      $M Gpack://application:,,,/MahApps.Metro;component/Styles/Accents/Blue.xaml?�6%   �� 5      $R Lpack://application:,,,/MahApps.Metro;component/Styles/Accents/BaseLight.xaml?�6%   �� 5      �� 5      $ Resources/book.jpg��6   BGVegetables  $	 0.8Q�6H   �� 5      $ Resources/MP900433052.JPG��6   BGGalet1  $	 0.8Q�6K   $ Uniform=�6Y   $	 Right=�6k   �� 5      $ Resources/MP910221704.JPG��6   BGGalet2  $	 0.8Q�6K   $ Uniform=�6Y   $	 Right=�6k   5      %    5       
   WindowCommands
 5"      +   %MahApps.Metro.Controls.WindowCommands  5#   
   .@�5$      �� $
��Aide��6   5%      .��5'      0   *MahApps.Metro.Controls.MetroContentControl  .��5*      � -   5         �Name$ 
LayoutRoot��5*   	    � RowDefinitions 5,      �� 5-      $
��Auto �6   $��86.503��6"   �� 5.      $
��Auto �6   $��60��6"   �� 5/      .+�55      � $��0��6
    56      �� 57      $K Epack://application:,,,/MahApps.Metro;component/Styles/FlatButton.xaml?�6   %     � ColumnDefinitions 59      �� 5:      $
��0.5* �6   �� 5;      $
��0.5* �6   .+�5=      �� -   5         ��Name$ BrandingBorder��5=        NavBrandingBorderStyle#��[ 5>      $��0��66   ._�5?      �� -   5         ��Name$ BrandingStackPanel��5?        BrandingStackPanelStyle#��[ 5@      .+�5A      �� -   5         ��Name$ ApplicationNameTextBlock��5A        ApplicationNameStyle#��[ 5B      $��35,0,0,0q�5C   #   ��6   �� *	PageTitle+��5D   	   �� 5E   
    
 Freeze4True 5G   
    �� Color$ 	#FF086F9E��5F   
   5G   
   5I   	   5J      5K      �� 5M      $��1��6!   ��61   ��  5N        Disconnected �� CornerRadius �� EndPoint �� 
StartPoint �� RelativeTransform �� CenterY �� CenterX �� CenterY �� CenterX �� Angle �� CenterY �� CenterX � Color � Offset ��Name  �� Orientation  appbar_people! !� Visual  	Connected  HeaderTextStyle!   MahApps.Metro.Controls.Tile 	 DefaultHyperlinkButtonStyle��%�  &	       &	 �    7 	 �� 5O      .R�5P      �  5Q   "   �� 5R   &   $��0.363* �67   �� 5S   &   $��0.425* �67   �� 5T   &   $��0.212* �67   .+�5V   
   �� $ border��6   $��1��6!   $��	0,-30,0,0q�61   $ 	0,0,10,10��6D   ��5W      �� 5X   $   $ 0.5,1&�68   $ 0.5,0&�6I    5Y   %   V� 5Z   &   .~�5[   '   �� $	 0.5Q�66   $	 0.5Q�6D   �� 5\   '   $	 0.5Q�65   $	 0.5Q�6C   �� 5]   '   $ 90Q�67   $	 0.5Q�6B   $	 0.5Q�6P   T� 5^   '   5_   '   .��5a   %   � $ 	#A8E5E5E5��62   $ 0.002Q�6D   � 5b   %   $ 	#A8E5E5E5��62   $ 1Q�6D   5c   %   ._�5e   #   �� $ button��6*   ���	#FF086F9E$��0,30,0,0q�6Q   ���	#1FD5D5D5$��18.667(�6�   ��6c   �� *LoginCommand+.��5f   $   �� $  
Horizontal=�6/   .+�5g   (   �� $��Se Connecter��62   $��Center=�6F   �� 5h   (   $��40��62   $��40��6>   ��5i   ,   !� 5j   0   #! �  6<   $
 Fill=�6e   5l   )   5m   $   5n      5o      5p      5q      �� 5r      .R�5s      �  5t   "   �� 5u   &   $��0.363* �67   �� 5v   &   $��0.425* �67   �� 5w   &   $��0.212* �67   .+�5y   "   �� $ border��6)   $��1��69   $��	0,-30,0,0q�6I   $ 	0,0,10,10��6\   ��5z   #   �� 5{   $   $ 0.5,1&�68   $ 0.5,0&�6I    5|   %   V� 5}   &   .~�5~   '   �� $	 0.5Q�66   $	 0.5Q�6D   �� 5   '   $	 0.5Q�65   $	 0.5Q�6C   �� 5�   '   $ 90Q�67   $	 0.5Q�6B   $	 0.5Q�6P   T� 5�   '   5�   '   .��5�   %   � $ 	#D4000000��62   $ 0Q�6D   � 5�   %   $ 	#CA000000��62   $ 1Q�6D   5�   %   ._�5�   &   �  5�   *   �� 5�   .   $��0.404* �6?   �� 5�   .   $��0.199* �6?   �� 5�   .   $��0.397* �6?   .+�5�   *   �� $��0��64   #���  6f   $��Right=�6�   $��Center=�6�   $��0q�6�   ��6E   �� *User.NomComplet+6�     5�   *   $��1��68   $��50��6H   $��50��6S   .��5�   +   �� ��62   �� *User.ImgUrl+5�      �� 5�   *   $��Se déconnecter��62   8��  $��40��6�   $��0��6�   $��Center=�6�   $��Center=�6�   $��2��6�   ��6K   �� *DisconnectCommand+6�   5�   '   5�   #   5�      5�      5�      ��5�      �� 5�      .R�5�      �� $��cc��6-   #���  6K   ��67   �� ��5�      �� 5�       $ 0.5,1&�64   $ 0.5,0&�6E   .��5�   !   � $ Black��6.   $ 0Q�6<   � 5�   !   $ White��6.   $ 1Q�6<   5�   !   5�      " �� Triggers" 5�      �� 5�   "   # �� Value$
# True��6K   $ �� Binding$ 6.   �� *
IsLogged+.a�5�   &   �� % �� 
TargetName$% cc��6-   & �� Value#& [ 6Y   ' �� Property' � ��5�   #   5�      5�      �� 5�      $
��Left=�6   $��0q�64   $��86.503��6?    
 MetroCircleButtonStyle#��� 
 6N   {�. ��6�   �� *AccueilCommand+.��5�      �� $��50��6   $��50��6   ( J� RenderTransformOrigin$( 0.5,0.5&�6(   z�5�      V� 5�   	   .~�5�   
   �� �� 5�   
   �� 5�   
   $ -35.034Q�6   T� 5�   
   5�   
   ��5�      !� 5�   	   $
 Fill=�6     appbar_home#! �  6$   5�      5�      5�      �� 5�      -   5        $ ContentBorder��5�      $��1��5�      $��2��5�      $��95,0,0,0q�5�      ._�5�      �� -   5        ) ��Name$) ContentFrame��5�      $��0,0,20,0q�5�      ��5�   +   �� *
ActiveView+5�      5�      �� 5�      -   5        $ PopupBorder��5�      $��3��6!   ���	#7FFFFFFFy�6I   �� *ActivePopupVisibility+._�5�      �� -   5        $) 
PopupFrame��5�      ��6)   �� *ActivePopup+5�      �� 5�      $
��Left=�6   $��5,0,0,0q�6*   $��1��6;   $��23��6H   ��5�      �� 5�      $ 0.5,1&�6   $ 0.5,0&�6,    5�      V� 5�   	   .~�5�   
   �� $	 0.5Q�6   $	 0.5Q�6'   �� 5�   
   $	 0.5Q�6   $	 0.5Q�6&   �� 5�   
   $	 0.5Q�6   $	 0.5Q�6(   T� 5�   
   5�   
   .��5�      � $ 	#CC119EDA��6   $ 1Q�6'   � 5�      $ 	#FF086F9E��6   5�      5�      5�      5�      5�      