
images_path = r'D:\Machine_Learning\gazoushori_kadai_data\learning_data\images'
labels_path = r'D:\Machine_Learning\gazoushori_kadai_data\learning_data\labels'
solo_path = r"D:\Machine_Learning\gazoushori_kadai_data\solo_4\sequence.0"
dataset_dir = r"D:\Machine_Learning\gazoushori_kadai_data\learning_data"
import os
import cv2
import json
import random
xmax_is_less_than_x_min = False #なんか学習時にこのエラーが出るんよね これいらんわ

def name_change(num,file_extension):
    i =num
    str_num = str(i)
    for k in range(0,5):
        if len(str_num) < 6:
            str_num = "0" + str_num

    str_num+=file_extension
    return str_num


def create_annotation_text(json_data,file_name):
    global xmax_is_less_than_x_min
    annotation_datas = json_data["captures"][0]["annotations"][0]["values"]
    #実験からわかったこと：yolov3へのラベルは　name x1 y1 x2 y2 の順で入れればいい！
    bboxes=[]
    for annotation_data in annotation_datas:
        label_name = annotation_data["labelName"]
        origin = annotation_data["origin"]
        dimention = annotation_data["dimension"]

        bbox= {"x1":0,"x2":0,"y1":0,"y2":0,"label_name":label_name}
        bbox["x1"] = origin[0] 
        bbox["y1"] = origin[1] 

        bbox["x2"] = origin[0] + dimention[0] + random.random()
        bbox["y2"] = origin[1] + dimention[1] + random.random()
        #↑のrandomは学習時のy_max is less than or equal to y_min for bbox対策
        
        bboxes.append(bbox)

        #学習のときにエラーが出る対策用
        if bbox["x2"] <= bbox["x1"]:
            xmax_is_less_than_x_min = True
    #yolov3用にテキストを保存する
    with open(file_name,"w",encoding="utf-8") as f:
        for bbox in bboxes:
            f.write(str(bbox["label_name"])+str(" ") +str(bbox["x1"]) +str(" ")+str(bbox["y1"])+str(" ")+str(bbox["x2"]) +str(" ")+str(bbox["y2"]) +str("\n"))




if __name__ == '__main__':



    #yolov3にはたぶん必要なやつ？　https://pystyle.info/pytorch-yolov3-how-to-train-custom-dataset/　を見た感じ
    test_txt_name = os.path.join(dataset_dir,"test.txt")
    train_txt_name = os.path.join(dataset_dir,"train.txt")
    test_txt = open(test_txt_name,"w")
    train_txt = open(train_txt_name,"w")

    for i in range(0,1000):
        png_name = "step" + str(i) +".camera.png"
        json_name = "step" + str(i) +".frame_data.json"
        print(os.path.join(solo_path,png_name))
        img = cv2.imread(os.path.join(solo_path,png_name))
        #cv2.imshow("aaa",img)
        json_data = json.load(open(os.path.join(solo_path,json_name),"r",encoding="utf-8"))

        fixed_png_name = name_change(i,".png")
        fixed_text_name = name_change(i,".txt")

        #ここからセーブ
        cv2.imwrite(os.path.join(images_path,fixed_png_name),img)
        create_annotation_text(json_data,os.path.join(labels_path,fixed_text_name))

        
        if i<900:
            #学習用データは多めに
            train_txt.write(fixed_png_name +"\n")
        else:
            test_txt.write(fixed_png_name+"\n")


    test_txt.close()
    train_txt.close()