function timeAgo(createdDate: string): string {
    const date = new Date(createdDate);
    const now = new Date();
    const diffInSeconds = Math.floor((now.getTime() - date.getTime()) / 1000);
    const diffInHours = Math.floor(diffInSeconds / 3600);
    const diffInDays = Math.floor(diffInHours / 24);
  
    if (diffInDays >= 7) {
      // More than a week ago: return date in "dd.mm.yyyy" format
      const day = String(date.getDate()).padStart(2, '0');
      const month = String(date.getMonth() + 1).padStart(2, '0'); // Months are zero-based
      const year = date.getFullYear();
      return `${day}.${month}.${year}`;
    } else if (diffInDays >= 1) {
      // More than a day ago: return number of days
      return `${diffInDays} day${diffInDays > 1 ? 's' : ''} ago`;
    } else {
      // Less than a day ago: return number of hours
      return `${diffInHours} hour${diffInHours > 1 ? 's' : ''} ago`;
    }
  }

export default timeAgo;