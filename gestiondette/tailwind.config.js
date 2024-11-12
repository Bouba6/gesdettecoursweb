/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    './Views/**/*.cshtml',
    './wwwroot/js/**/*.js',
    './wwwroot/css/**/*.css'
  ],
  theme: {
    extend: {
      width: {
        'xgg': '700px',
      },
    },


  },
  plugins: [],
}

