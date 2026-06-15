export const formatDate=(dateString)=>{
    return new Date(dateString).toLocaleDateString("en-US");
}

export const formatForHtmlDate = (dateString) =>  {
   return dateString?.split("T")[0];
}
