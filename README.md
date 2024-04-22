# 自動小銭集計システム
動作画面↓

https://github.com/TANIGUCHIREI/-Digital_gazousyori_kadai/assets/120480219/60a6706b-6652-48cc-959a-141043ee5691

## 背景
大学での講義「ディジタル画像処理」の最終課題にて、「画像処理技術を用いてなにかシステムを作成せよ」という課題が与えられました。
当時自宅にたまりにたまっていた小銭を集計する必要があったのですが、そのために画像処理技術を利用できないかと考えました。

## 作成手順

※詳細は[ミニレポート](https://github.com/TANIGUCHIREI/-Digital_gazousyori_kadai/blob/main/%E3%83%87%E3%82%A3%E3%82%B8%E3%82%BF%E3%83%AB%E7%94%BB%E5%83%8F%E5%87%A6%E7%90%86%E3%83%9F%E3%83%8B%E3%83%AC%E3%83%9D%E3%83%BC%E3%83%8808D20080%E8%B0%B7%E5%8F%A3%E4%BB%A4.pdf)に記載しています。

小銭のアノテーションデータが存在しなかったため、自作を行いました。

### 1.Blenderで小銭の3Dモデル(BOOTHにて購入)を編集

![image](https://github.com/TANIGUCHIREI/-Digital_gazousyori_kadai/assets/120480219/25b92053-b81e-49f4-aab9-352e801091ec)

### 2.UnityにてPerception Assetを用いてアノテーションデータを自動で作成（計1000枚）

カメラの角度や金属の質感、汚れ具合などはノーマルマップ等をランダムに変更して表現しました。

![image](https://github.com/TANIGUCHIREI/-Digital_gazousyori_kadai/assets/120480219/33f7aa0d-b0ac-4207-936a-556be85894e0)
![image](https://github.com/TANIGUCHIREI/-Digital_gazousyori_kadai/assets/120480219/09bbad51-0ce5-49b4-bf2a-3e41d3954c82)

### 3.YOLOv3を用いて学習

↓学習における損失の減少

![image](https://github.com/TANIGUCHIREI/-Digital_gazousyori_kadai/assets/120480219/6156d1ac-dda8-4bec-8d07-cb9e8dfbebab)

### 4.静止画像を用いてテスト確認

![image](https://github.com/TANIGUCHIREI/-Digital_gazousyori_kadai/assets/120480219/6116682a-3678-492a-ab85-955b4bea1ea0)

ネットから取得した現実世界の小銭に対して適切に処理できていることを確認

### 5.スマホからの動画をリアルタイムで処理・合計金額を計測するようなシステムを作成(ivCamを使用)







## 使用したスクリプト
#### ・capturetest.cs ・・・Unityで使用
#### ・convertToLearningSet.py ・・・jsonデータをtxtに変換するのに使用
#### ・testuse.ipynb ・・・webカメラでリアルタイムに小銭を分類するのに使用
#### ・Camera 2023-02-04 23-15-30.mp4　・・・リアルタイム動作している様子の動画

UnityプロジェクトとyoloV3のモデルはどちらも数GBあり、回線が貧弱なこともあってアップロードできませんでした・・・すみません！
