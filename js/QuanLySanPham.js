
	

 function myFunction()
 {
	const dongia = document.getElementById("txtDongia");
  	const soluong = document.getElementById("txtSoluong");
 	const sl = Number(soluong.value)
 	const dg = Number(dongia.value)
 	 if (isNaN(Number(dongia.value)) || isNaN(Number(soluong.value)) ) {
	    alert("Giá trị nhập vào không phải là một số.");
	    event.preventDefault(); // Ngăn chặn form submit
	    
 	 }
  	if (sl <=0 || dg <= 0)
  	{
	    alert("Số lượng phải lớn hơn 0");
	    event.preventDefault(); // Ngăn chặn form submit
 	 }
}

