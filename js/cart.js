function checkbox() {
                    	  const checkboxall = document.getElementById("checkboxall");
                    	  const allCheckboxes = document.querySelectorAll('.checkboxproducl');
                    		if(checkboxall.checked == true){
                    			allCheckboxes.forEach(checkbox => checkbox.checked = true)
                    			}
                    		else{
                    			allCheckboxes.forEach(checkbox => checkbox.checked = false)}
                    	  }
                    	  
                    	  
                    	  
                    	  
