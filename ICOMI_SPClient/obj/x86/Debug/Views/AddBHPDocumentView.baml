   M S B A M L   `   `   `  ����H  DMahApps.Metro, Version=0.7.1.0, Culture=neutral, PublicKeyToken=nullV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5       ICOMI_SPClient-   'ICOMI_SPClient.Views.AddBHPDocumentView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Msdk=http://schemas.microsoft.com/winfx/2006/xaml/presentation/sdk     5      PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        � Triggers  5
      :� 5   
    :� RoutedEvent$ FrameworkElement.Loaded��6   .[�5      �� .��5      �� .��5      W�  �� TargetProperty$ Opacity�6&    g� Duration$ 00:00:01D�5   2    W� From$ 0X�5   2   	 W� To$ 1X�5   2   5      5      5      :� 5   
   $ FrameworkElement.Unloaded��6   .[�5      �� .��5      �� .��5      W� $ Opacity�6&   $ 00:00:01D�5   2   $ 1X�5   2   $ 0X�5   2   5      5      5       � 	Resources 5        �� MergedDictionaries �� Source�� 5"       5#      �� 5$      $H Bpack://application:,,,/MahApps.Metro;component/Styles/Colours.xaml?�6%   �� 5%      $F @pack://application:,,,/MahApps.Metro;component/Styles/Fonts.xaml?�6%   �� 5&      $I Cpack://application:,,,/MahApps.Metro;component/Styles/Controls.xaml?�6%   �� 5'      $e _pack://application:,,,/MahApps.Metro;component/Styles/Controls.AnimatedSingleRowTabControl.xaml?�6%   �� 5(      $I Cpack://application:,,,/MahApps.Metro.Resources;component/Icons.xaml?�6%   �� 5)      $M Gpack://application:,,,/MahApps.Metro;component/Styles/Accents/blue.xaml?�6%   �� 5*      $R Lpack://application:,,,/MahApps.Metro;component/Styles/Accents/BaseLight.xaml?�6%   %    5,      	 � RowDefinitions	 5.      �� 5/      $��0.081* �6   �� 50      $��0.919* �6   .+�52      ��    HeaderTextStyle#��[  6   $
��Wrap=�53   $   $>��8Ajouter un document dans les bonnes pratiques d'hygiène��54   $   
 �� TextAlignment$

 Left=�55      $��
10,10,30,0q�56   $   � 57      $��1��6    58        ParagraphTextStyle �� 
TargetType �� Property �� Value��%  &	       �� 59      " ��5:      .�5;      ��  � ��$
 Left=�5<      �� 5=       � ��$	 500��5>      �� 5?       �@��Padding$ 3,6,0,3q�5@      �� 5A       � ��$ 16(�5B      �� 5C       � ��$
 Wrap=�5D      5E      5F       � ColumnDefinitions 5G      �� 5H      $��0.252* �6   �� 5I      $��0.748* �6   	 5K      �� 5L      $
��79.3 �6   �� 5M      $	��382 �6   �� 5N      .+�5P      �� $��0��6   ���	#AA000000$��2��62   $��3��6F    �� CornerRadius$ 10,10,10,10��6W   $��-5,5,5,5q�6r    J� Opacity$	 0.7Q�6�   �� 5Q      #��[ 6   $
��Wrap=�5R   $   $&�� Etape 1 : Sélection du document��5S   $   $

 Left=�5T      $��10q�5U   $   
�������60   � 5V   
   $��28��6   $��0��6   $��1��6(   $��
210,5,10,0q�5W       5X      �� 5Y      $��0.896* �6#   �� 5Z      $��0.104* �6#   .+�5\      �� -   5        $�� ��5\        SearchButtonStyle#��[ 5]   %   $��0q�5^   %   $��2��5_   %   $��1��5`   %   �� 5a      -   5         ��Name$ FileTextBox��5a      $��0,0,6,0q�6,   5b      �� 5c   
   #��[ 6   $��1��5d   
   $
��Wrap=�5e   $   $,��&Etape 2 : Caractérisation du document��5f   $   $

 Left=�5g      $��
10,10,0,10q�5h   $   
�������68   � 5i      $��1��6	   $��1��6    5j      �� 5k      $	��200 �6   �� 5l      	 5n      �� 5o      �� 5p      �� 5q      �� 5r      �� 5s      �� 5t      .+�5w      ��   ControlLabelStyle#��[ 6   $��0��5x   
   $
��Wrap=�5y   $   $��Titre��5z   $   $
 Right=�5{      $��	10,10,0,0q�5|   $   
�������67   �� 5}      #��[ 6   $��1��5~   
   $
��Wrap=�5   $   $��
Producteur��5�   $   $
 Right=�5�      $��	10,10,0,0q�5�   $   
�������67   �� 5�      #��[ 6   $��2��5�   
   $
��Wrap=�5�   $   $��Rubrique��5�   $   $
 Right=�5�      $��	10,10,0,0q�5�   $   
�������67   �� 5�      #��[ 6   $��3��5�   
   $
��Wrap=�5�   $   $
��Type��5�   $   $
 Right=�5�      $��	10,10,0,0q�5�   $   
�������67   �� 5�      #��[ 6   $��4��5�   
   $
��Wrap=�5�   $   $��	Sous Type��5�   $   $
 Right=�5�      $��	10,10,0,0q�5�   $   
�������67   �� 5�      #��[ 6   $��5��5�   
   $
��Wrap=�5�   $   $ ��Commentaires (facultatifs)��5�   $   $
 Right=�5�      $��	10,10,0,0q�5�   $   
�������67   �� 5�      -   5        $ Titre��5�      $�� ��6   $��1��5�   
   $��0��5�   
   $��	10,0,10,0q�5�   *   $��40��5�   *   �� 5�      -   5        $ 
Producteur��5�      $�� ��6"   $��1��5�   
   $��1��5�   
   $��	10,0,10,0q�5�   *   $��40��5�   *   �� 5�      -   5         ��Name$ Rubrique��5�      $��1��5�      $��2��5�   
   $��	10,0,10,0q�5�   
   $��40��5�   
   .@�5�      �� $ ��Lutte contre les nuisibles��6+   �� 5�      $��Qualité de l'eau��6+   �� 5�      $��Formation du personnel��6+   �� 5�      $��Santé��6+   �� 5�   	   $��Tenue��6   5�      �� 5�      -   5        $
 Type��5�      $��1��5�      $��3��5�   
   $��	10,0,10,0q�5�   
   $��40��5�   
   .@�5�      �� $��Contrat��6+   �� 5�      $
��Plan��6+   �� 5�      $��Fiche Descriptive��6+   �� 5�      $��
Procédure��6+   �� 5�   	   $��Autre document��6   5�      �� 5�      -   5        $ SousType��5�      $��1��5�      $��4��5�   
   $��	10,0,10,0q�5�   
   $��40��5�   
   .@�5�      �� $��Preuve��6+   �� 5�      $��	Intention��6+   5�      �� 5�      -   5        $ Commentaires��5�      $�� ��6$   $��1��5�   
   $��5��5�   
   $��	10,0,10,0q�5�   *   $��40��5�   *   5�      �� 5�      #��[ 6   $��2��5�   
   $��0��5�   
   $
��Wrap=�5�   $   $/��)Etape 3 : Insertion dans la bibliothèque��5�   $   $

 Left=�5�      $��
10,10,0,10q�5�   $   
�������68   � 5�      $��2��6	   $��1��6   $��210,30,10,0q�6&   .+�5�      �� .+�5�      �� -	   5        $0��*Insérer le document dans la bibliothèque��5�      6A   .   (MahApps.Metro.Controls.ProgressIndicator  5�      -
   5          Name$ ProgressInsert��5�   !   $y�Hidden=�69     AccentColorBrush   ProgressColour# [ 6M   $��6��5�   5   $	��600��5�   5   5�      5�      5�      5�      