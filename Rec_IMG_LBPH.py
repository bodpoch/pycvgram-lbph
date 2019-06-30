import cv2  # Importing the opencv
import numpy as np  # Import Numarical Python
import NameFind

# --- import the Haar cascades for face and eye detection

face_cascade = cv2.CascadeClassifier('C:/defold/pycvgram/Haar/haarcascade_frontalface_alt2.xml')
eye_cascade = cv2.CascadeClassifier('C:/defold/pycvgram/python4tele/Haar/haarcascade_eye.xml')
spec_cascade = cv2.CascadeClassifier('C:/defold/pycvgram/Haar/haarcascade_eye_tree_eyeglasses.xml')

# FACE RECOGNISER OBJECT
LBPH = cv2.face.LBPHFaceRecognizer_create(2, 8, 8, 8, 15)

# Load the training data from the trainer to recognise the faces
LBPH.read("C:/defold/pycvgram/recondata/trainingDataLBPH.xml")

# ------------------------------------  PHOTO INPUT  -----------------------------------------------------

img = cv2.imread('C:/defold/pycvgram/MYIMG.jpg')  # ------->>> THE ADDRESS TO THE PHOTO

gray = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)  # Convert the Camera to gray
faces = face_cascade.detectMultiScale(gray, 1.3, 4)  # Detect the faces and store the positions
#print(faces)

for (x, y, w, h) in faces:  # Frames  LOCATION X, Y  WIDTH, HEIGHT

    Face = cv2.resize((gray[y: y + h, x: x + w]), (110, 110))  # The Face is isolated and cropped

    ID, conf = LBPH.predict(Face)  # LBPH RECOGNITION
    confval = 4
    
    if (conf < confval):
        NAME = NameFind.ID2Name(ID, conf)
        NameFind.DispID(x, y, w, h, NAME, gray)
        print('\U000026A0 ID: ', ID, NAME, '|', "{:.2f}".format(100 - ((conf - 1) / (confval - 1)) * 100), ' %')
    else:
        NAME = NameFind.ID2Name(0, conf)
        NameFind.DispID(x, y, w, h, NAME, gray)
        print('\U0000274C Not recognised')

cv2.imshow('LBPH Photo Recogniser', gray)  # IMAGE DISPLAY
cv2.imwrite('C:/defold/pycvgram/OUTIMG.jpg', gray)
cv2.destroyAllWindows()