# Pycvgram

Pycvgram-lbph is a simple bot project using C# for Telegram.Bot API 
and Python with OpenCV library (4.0 or higher) for face processing. Check out my another project for
tutorial with face recognition process: https://github.com/bodpoch/spoofing-protected-lbph-opencv.

![alt text](https://raw.githubusercontent.com/bodpoch/pycvgram-lbph/master/overview.png)

# Files

**Face_Capture_With_Rotate.py:** Running this file will capture 50 images of a person infront of with illumination and rotation correction.

**NameFind.py:** This file contains all the functions.

**Trainer.py:** This file will train LBPH recognition algorithm using the images in the photodb folder.

**Rec_IMG_LBPH.py:** Script to recognise faces in the sample image using Haar-cascades and LBPH

**Rec_IMG_LBPH_DLD.py** Script to recognise faces in the sample image using Deep Learning detection and LBPH

**MYIMG.jpg:** sample image

**OUTIMG.jpg:** processed image

**Names.txt:** contains ID and NAME of all persons in the database

**res10_300x300_ssd_iter_140000.caffemodel** pre-trained caffe framework model for face detection

# Folders

**photodb:** Contains the images that will be used to train the recogniser

**Haar:** Contains Haar Cascades of OpenCV used in the applications

**recondata:** Contains the saved XML file with LBPH training data

**packages:** Nuget Telegram Api packages

**TeleFace:** Telegram Bot App folder
