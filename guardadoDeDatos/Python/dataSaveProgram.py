import tkinter as tk
import re
from tkinter import messagebox
import mysql.connector as mysqlCon


def insertRegistry(name, lastname, age, height, phone, gender):
    try:
        connection = mysqlCon.connect(
            host = "localhost",
            port = "3306",
            user = "root",
            password = "",
            database = "michaelbay"
        )
        cursor = connection.cursor()
        query = "insert into tabla (nameVal, lastName, telephone, height, age, gender) values (%s, %s, %s, %s, %s, %s)"
        values = (name, lastname, phone, height, age, gender)
        cursor.execute(query, values)
        connection.commit()
        cursor.close()
        connection.close()
        messagebox.showinfo("info", "values saved succesfully on database")
    except mysqlCon.error as err:
        messagebox.showerror("Error", f"error while saving data: {err}")


def clear():
  tbName.delete(0,tk.END)
  tbLastName.delete(0,tk.END)
  tbHeight.delete(0,tk.END)
  tbPhone.delete(0,tk.END)
  tbAge.delete(0,tk.END)
  varGender.set(0)
  	
#Validations
def isValidInt(val):
    try:
      int(val)
      return True
    except ValueError:
      return False

def isValidFloat(val):
    try:
      float(val)
      return True
    except ValueError:
      return False
  
def isValidPhone(val):
     return val.isdigit() and len(val) == 10

def isValidText(val):
   return bool(re.match("^[a-zA-Z\s]+$", val))


def save():
  names = tbName.get()
  lastN = tbLastName.get()
  age   = tbAge.get()
  phone = tbPhone.get()
  height= tbHeight.get()
  gender= ""
  if varGender.get() == 1:
    gender = "Male"
  elif varGender.get() == 2:
    gender = "Female"
  #validate data
  
  if(isValidInt(age) and isValidFloat(height) and isValidPhone(phone) and isValidText(names) and isValidText(lastN)):
      #database savetry
      insertRegistry(names, lastN, age, height, phone, gender)

      # Generate string
      data = "Names: " + names + "\nLast Names: " + lastN + "\nAge: " + age + "\nPhone: " + phone + "\nHeight: " + height + "\nGender: " + gender
      with open("3O2024Data.txt", "a") as file:
        file.write(data + "\n\n")
      # Show data message
      messagebox.showinfo("Info" + "Data saved succesfully\n\n", data)
  else:
     messagebox.showerror("Error", "Couldn't save data\n\nBadFormat")
     
   #clean data after saving
  clear();  


# Window creation
window = tk.Tk()
window.geometry("480x640")
window.title("Form")

# RadioBttn variable
varGender = tk.IntVar()

# Tags & entry fields
lbName = tk.Label(window, text="Names: ")
lbName.pack()
tbName = tk.Entry()
tbName.pack()
lbLastName = tk.Label(window, text="Last Name:")
lbLastName.pack()
tbLastName = tk.Entry()
tbLastName.pack()
lbAge = tk.Label(window, text="Age:")
lbAge.pack()
tbAge = tk.Entry()
tbAge.pack()
lbPhone = tk.Label(window, text="Phone Number:")
lbPhone.pack()
tbPhone = tk.Entry()
tbPhone.pack()
lbHeight = tk.Label(window, text="Height:")
lbHeight.pack()
tbHeight = tk.Entry()
tbHeight.pack()
lbGender = tk.Label(window, text="Gender:")
lbGender.pack()
rbMale = tk.Radiobutton(window, text = "Male", variable=varGender, value=1)
rbMale.pack()
rbFemale = tk.Radiobutton(window, text = "Female", variable=varGender, value=2)
rbFemale.pack()

btnClear = tk.Button(window, text = "Clear Values", command=clear)
btnClear.pack()
btnSave  = tk.Button(window, text = "Save Values", command=save)
btnSave.pack()


window.mainloop()
