   M S B A M L   `   `   `  ���� O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  L HAnalyticalScalper, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:AnalyticalScalper.ViewModelsAnalyticalScalper.ViewModels "   AnalyticalScalper.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     6sys&clr-namespace:System;assembly=mscorlib     8c*clr-namespace:AnalyticalScalper.ViewModels     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -     � Title$  
MainWindow��$	��550��$	��825�� � WindowState$ 	Maximized=����	#FF858585 � 	Resources , &AnalyticalScalper.ViewModels.ViewModel   ViModel��%   &	           � DataContext ��  �� Source# [  .���  � RowDefinitions �� $
��Auto ��� $��2* ��� $��1* ��� $��1* � � ColumnDefinitions �� �� $
��Auto �.+��� $��1��$��5q����	#FF646464�� $��2��$��5q����	#FF646464�� $��3��$��5q����	#FF646464�� $��1��$��5q�$	��0.5q�
���   �._��� $��5q�$	��0.5q�
���   �._�� .+��� ����  �� ElementName$ CanvasPrice�� �� Path$ Margin�.+��� 		 �� X1$	 0��$	��0.5��
�������	
 �� Y1
 �� $. (FactoryCharts.PriceChart.YCoordinateLine� �� Mode$ OneWay=�	 �� X2 �� $ CanvasPrice��$ ActualWidth�	 �� Y2 �� $. (FactoryCharts.PriceChart.YCoordinateLine�$ OneWay=��  �� Left �� $ CanvasPrice��$ ActualWidth�
 �� Top �� $. (FactoryCharts.PriceChart.YCoordinateLine�$ OneWay=�.+��� $��2,0,0,0q�
����������� $+ %FactoryCharts.PriceChart.CurrentValue�$ OneWay=��� $��1��$ 0��
����������� $ CanvasPrice��$ ActualWidth� �� Bottom �� $* $FactoryCharts.PriceCurrentPosition.Y�$ OneWay=��� -    ��Name$ CanvasPrice��$��1q�$��10,10,70,10q���Z� ���� $, &FactoryCharts.PriceChart.ElementsChart� �� IsAsync . $ OneTime=����� .R��� ��Z� ���� .R��� �� -    ��Name$  AdditionalInformationPanel������ $ CanvasPrice��$ Margin�.+��� -    ��Name$ 
TapeTrades�� J� Opacity$
 0.15Q����	#7F4B4B4B �� $B <FactoryCharts.MultiDependetPanelTrades.FullyDependentPanel.X�$ OneWay=� �� $B <FactoryCharts.MultiDependetPanelTrades.FullyDependentPanel.Y�$ OneWay=�.+��� ���� $? 9FactoryCharts.TapeTradesEveryVolume.drawElementCollection����� .R�A� ���� .R��� $��	0,15,10,0q� �� Orientation$ 
Horizontal=�$��RightToLeft=��� ���� $@ :FactoryCharts.TapeTradesSumOperation.drawElementCollection����� .R�A� ���� .R��� $��	0,15,10,0q�$ 
Horizontal=�$��RightToLeft=��� -    ��Name$ 
OfferPanel��$
��Auto��$��Center=�
���P�����	#FFFFD2C2$��0,0,0,1q�$��Normal"�$��9(����	#598F8F8F���� $) #FactoryCharts.OfferPanel.InputValue�$ OneWay=� �� $&  FactoryCharts.OfferPanel.X_panel� �� $% FactoryCharts.OfferPanel.Y_tape��� -   $ BidPanel��$
��Auto��$��Center=�
���2�2����	#FFD5FFD5$��0,1,0,0q�$��Normal"�$��9(����	#598F8F8F���� $' !FactoryCharts.BidPanel.InputValue�$ OneWay=� �� $$ FactoryCharts.BidPanel.X_panel� �� $# FactoryCharts.BidPanel.Y_tape��� ���	#FF5A7373
�������$
��Auto��$
��Auto��$
��Left=�$��Bottom=�$��0q�.��Price�� $��3��$��5q�$	��0.5q�
���   �._��  �� �� .+��� $��2��$��5q�$	��0.5q�
���   �._�� .+��� ���� $ CanvasSpeedPrice��$ Margin�.+��� $	 0��$	��0.5��
�������
 �� $7 1FactoryCharts.SpeedTradeTickChart.YCoordinateLine�$ OneWay=� �� $ CanvasSpeedPrice��$ ActualWidth� �� $7 1FactoryCharts.SpeedTradeTickChart.YCoordinateLine�$ OneWay=��  �� $ CanvasSpeedPrice��$ ActualWidth� �� $7 1FactoryCharts.SpeedTradeTickChart.YCoordinateLine�$ OneWay=�.+��� $��2,0,0,0q�
����������� $4 .FactoryCharts.SpeedTradeTickChart.CurrentValue��� -   $ CanvasSpeedPrice��$��10,10,70,10q�$��1q����� $5 /FactoryCharts.SpeedTradeTickChart.ElementsChart� . $ OneTime=���Z� ���� .R��� ��Z� ���� .R��� �� $��Bottom=����	#7FEEAFDD���� $+ %FactoryCharts.SpeedTradesPerAvChart.Y�$ OneWay=����� $ CanvasSpeedPrice��$ ActualWidth����� $ CanvasSpeedPrice��$ Margin��� $��Bottom=����	#4CAFEEEE���� $+ %FactoryCharts.SpeedTradesDayAvChart.Y�$ OneWay=����� $ CanvasSpeedPrice��$ ActualWidth����� $ CanvasSpeedPrice��$ Margin�� -    �Name$ 	GridTrend��$��Right=�$��50��.+��� -	   $ CanvasTrend��.+��� 
������$��1������ $ 	GridTrend��$ Width� �� $&  FactoryCharts.MarketTrendChart.Y�$ OneWay=��� 
�������$��Center=�$��1���� $	��Top=�$
��Left=�$��	-50,0,0,0q�
����������� $/ )FactoryCharts.MarketTrendChart.TrendValue�$ OneWay=��� ���	#FF5A7373
�������$
��Auto��$
��Auto��$
��Left=�$��Bottom=�$��5q�.��Speed price�� $��2��$��5q�$	��0.5q�
���   �._��� $��5q�$	��0.5q�
���   �._�� .+��� -
   $ CanvasTrendGisgram��$��1q�$��10,10,70,10q�$ 50Q���Z� ���� $9 3FactoryCharts.MarketTrendGisgramChart.ElementsChart� . $ OneWay=����� .R��� ��Z� ���� .R��� �� ���	#FF5A7373
�������$
��Left=�$��Bottom=�$��5q�.��Trend�� -    ��Name$ TradesPanel��$��1��$��1��$��10��$��5,5,10,5q�
���   �$	��0.5q�$��Stretch=�$ 1Q�   	ForeBrush �� Color��%   &	       �� $ 	Gainsboro��._��� ���	#FF646464.+��� $ 
Horizontal=�.+��� .+��� $��25��$��Center=�$��10(�#��[ $
��Pos:���� $��65��$��Center=�#��[ ���� $  TraderData.CurrentPosition�$ OneWay=��� .+��� $��65��$��Center=�$��10(�#��[ $��Price:���� $��65��$��Center=�#��[ ���� $ TraderData.PricePosition�$ OneWay=��� .+��� $��65��$��Center=�$��10(�#��[ $��Variational:���� $��65��$��Center=�#��[ ���� $ TraderData.Varitional�$ OneWay=��� .+��� $��65��$��Center=�$
��Left=�$��10(�#��[ $��Profit (point):���� $��65��$��Center=�$
��Left=�#��[ ���� $ TraderData.ProfitTotal�$ OneWay=��� .+��� $��75��$��Center=�$
��Left=�$��10(�#��[ $��Count trades:���� $��65��$��Center=�$
��Left=�#��[ ���� $ TraderData.CountTrades�$ OneWay=��� 
���   �$��0,0.5,0,0.5q�$	��0,5q�._��� $ 
Horizontal=�   	H_element  	W_element  	LabHeader �� 
TargetType �� Property �� Value��%�   &	       &	      &	      7  7  7  X�25Q�X�70Q��� " ��.���  � ��8   ��  � ��8  ��  � ��$ Center=���  � ��$ Stretch=���  � ��$ Center=���  � ��$ 10(���  � ��8  .+��� .+��� #��[ $��SP���� #��[ ���� $ TraderData.ProfitValue��� .+��� #��[ $��SL���� #��[ ���� $ TraderData.LossValue��� -   $ CanvasGlass��$��1��$��10�����	#00000000