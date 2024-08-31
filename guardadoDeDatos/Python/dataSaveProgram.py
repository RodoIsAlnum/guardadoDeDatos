import tkinter as tk
from tkinter import messagebox

def clear():
  tbName.delete(0,tk.END)
  tbLastName.delete(0,tk.END)
  tbHeight.delete(0,tk.END)
  tbPhone.delete(0,tk.END)
  tbAge.delete(0,tk.END)
  varGender.set(0)
  	
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
  # Generate string
  data = "Names: " + names + "\nLast Names: " + lastN + "\nAge: " + age + "\nPhone: " + phone + "\nHeight: " + height + "\nGender: " + gender
  with open("3O2024Data.txt", "a") as file:
    file.write(data + "\n\n")
  # Show data message
  messagebox.showinfo("Info" + "Data saved succesfully\n\n", data)



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
