   M S B A M L   `   `   `  ����  ICOMI_SPClient#    ICOMI_SPClient.Views.TaskView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      $	��500��5      $	��610��6   
�������  � 	Resources  5       �� MergedDictionaries �� Source   HighlightBrush2  HighlightDarkColor �� Color�� 5	   
    5
      �� 5      $H Bpack://application:,,,/MahApps.Metro;component/Styles/Colours.xaml?�6%   �� 5      $F @pack://application:,,,/MahApps.Metro;component/Styles/Fonts.xaml?�6%   �� 5      $I Cpack://application:,,,/MahApps.Metro;component/Styles/Controls.xaml?�6%   �� 5      $e _pack://application:,,,/MahApps.Metro;component/Styles/Controls.AnimatedSingleRowTabControl.xaml?�6%   �� 5      $I Cpack://application:,,,/MahApps.Metro.Resources;component/Icons.xaml?�6%   �� 5      $M Gpack://application:,,,/MahApps.Metro;component/Styles/Accents/blue.xaml?�6%   �� 5      $R Lpack://application:,,,/MahApps.Metro;component/Styles/Accents/BaseLight.xaml?�6%   %*   &	        7  �� 5      8   5      5       J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5   
    � RowDefinitions 5      �� 5   
   $��0.054* �6   �� 5   
   $��0.172* �6   �� 5   
   $��0.667* �6   �� 5   
   $��0.107* �6   .+�5!      � $��2��6   
�������$��10q�6+    � ColumnDefinitions 5"   
   �� 5#      $��0.765* �6   �� 5$      $��0.235* �6    5&   
   �� 5'      $��70 �6   �� 5(      $��70 �6   �� 5)      $��70 �6   �� 5*      $��70 �6   �� 5+      .+�5-   
   �� $��2��6   $��4��6%   $��2q�66   ���	#FFADA8A8��5.      �� 5/       �� EndPoint$ 0.5,1&�6&    �� 
StartPoint$ 0.5,0&�67   .��50      � 	 � Color$	 White��6#   
 � Offset$
 0Q�61   � 51      $	 	#FFEBE7E7��6#   $
 1Q�65   52      54      �� 55   
    �� Orientation$ 
Horizontal=�6   $��0��6.   $��10,10,10,10q�6;   $��Right=�6P   $��Center=�6l   .+�56      ��   HeaderTextStyle#���  6   $
��Wrap=�57   $   $��
Opérateur��58   $   $��	10,10,0,0q�59   $   
���   �67   �� 5:      $	��266��6   $��10,0,0,0q�6"    �� 
IsReadOnly . ��64   �� *LaTache.NomComplet+6X   5;      �� 5<   
   $ 
Horizontal=�6   $��1��6.   $��10,10,10,10q�6;   $��Right=�6P   $��Center=�6l   .+�5=      �� #���  6   $
��Wrap=�5>   $   $��Titre��5?   $   $��	10,10,0,0q�5@   $   
���   �67   �� 5A      $	��266��6   $��10,0,0,0q�6"    . ��64   �� *LaTache.Title+6S   5B      �� 5C   
   $ 
Horizontal=�6   $��2��6.   $��2��6;   $��	0,10,10,0q�6L   $��Right=�6_   $	��Top=�6{   $	��130��6�   .+�5D      �� #���  6   $
��Wrap=�5E   $   $��Description��5F   $   $��	10,10,0,0q�5G   $   
���   �67   �� 5H      $	��266��6   $��10,0,0,0q�6#    .  �� TextWrapping$
 Wrap=�6l   " �� VerticalScrollBarVisibility$ Visible=�6�    �� AcceptsReturn . $	��121��6�   ��65   �� *LaTache.Description+6�   5I      5J      �� 5K      $
��Left=�6   $��223.97��6+   $��10,3,0,3q�6:   ��5L   
   �� 5M      $ 0.5,1&�6"   $ 0.5,0&�63    �� RelativeTransform 5N      V� 5O      .~�5P      ��  �� CenterY$	 0.5Q�6)    �� CenterX$	 0.5Q�67   �� 5Q       �� CenterY$	 0.5Q�6(    �� CenterX$	 0.5Q�66   �� 5R       �� Angle$ 90Q�6*    �� CenterY$	 0.5Q�65    �� CenterX$	 0.5Q�6C   T� 5S      5T      .��5V      � $	 	#FFFF5D00��6   $
 1Q�61   � 5W      #	 �  6   5X      5Z      �� 5[        ApplicationNameStyle#���  6   $
��Wrap=�5\      $��Tâche��5]      
���   �$��1��5^      $��10,0,0,0q�6$   $��Stretch=�66   �� 5`      $��3��6   $
��Wrap=�6   
���  ��$��10,0,0,0q�6]   ��61   �� *	ErrorText+6]   �� 5a      $��3��6   $ 
Horizontal=�6   $��Center=�67   .+�5b   
   �� $��10,6.5,20,6.3q�6   $��Déclarer un problème��6(   $	��150��6G   ��6T   �� *DeclarerProbleme+�� 5c   
   $��10,6.5,20,6.3q�6   $��Tâche effectuée��6(   $	��150��6B   ��6N   �� *EffectuerCommand+�� 5d   
   $��Annuler��6   $��3��6#   $��10,6.5,20,6.3q�5e      $��Right=�6   $	��150��67   ��6C   �� *AnnulerCommand+5f      5g      