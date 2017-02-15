import random

if __name__=="__main__":
    password=""
    input_1=int(input("Please Enter Number Of Random"))
    for i in range(input_1):
        password=password+str(random.randint(0,9))
    print(password)
        
