<template>
  <div id="app">
      <router-view/>
  </div>
</template>
<script>
export default {
  name: 'App',
  components: {
  },
}
</script>
<style>
    #app {
  margin: 0;
  padding: 0;
  width: 100vw; 
}
.header {
  width: 100%; 
  background-color: #2c3e50; 
  color: white; 
  text-align: center;
}
</style>