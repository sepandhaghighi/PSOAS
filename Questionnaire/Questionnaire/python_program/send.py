import os
import shutil
import time
import sys
def file_delete(counter):
    try:
        shutil.rmtree("image",ignore_errors=True)
        os.remove("info.xlsx")
        sys.exit()
    except:
        time.sleep(1)
        if counter<7:
            print(counter)
            file_delete(counter+1)
        else:
            sys.exit()


if __name__=="__main__":
    print("Please Wait . . . ")
    file_delete(1)

